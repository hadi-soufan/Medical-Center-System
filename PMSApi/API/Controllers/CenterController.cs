using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CenterController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CenterController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Center>>> GetCenters()
        {
            return await _context.Centers.Where(c => !c.IsDeleted).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Center>> GetCenter(Guid id)
        {
            var center = await _context.Centers.FindAsync(id);

            if (center == null)
            {
                return NotFound();
            }

            return center;
        }

        [HttpPost]
        public async Task<ActionResult<Center>> CreateCenter(Center center)
        {
            _context.Centers.Add(center);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCenter), new { id = center.CenterId }, center);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCenter(Guid id, Center center)
        {
            if (id != center.CenterId)
            {
                return BadRequest();
            }

            _context.Entry(center).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CenterExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCenter(Guid id)
        {
            var center = await _context.Centers.FindAsync(id);
            if (center == null)
            {
                return NotFound();
            }

            center.IsDeleted = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CenterExists(Guid id)
        {
            return _context.Centers.Any(e => e.CenterId == id);
        }
    }
}
