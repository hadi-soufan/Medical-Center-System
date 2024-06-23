using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FloorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FloorController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Floor>>> GetFloors()
        {
            return await _context.Floors.Include(f => f.Department).Where(f => !f.IsDeleted).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Floor>> GetFloor(Guid id)
        {
            var floor = await _context.Floors.Include(f => f.Department).FirstOrDefaultAsync(f => f.FloorId == id);

            if (floor == null)
            {
                return NotFound();
            }

            return floor;
        }

        [HttpPost]
        public async Task<ActionResult<Floor>> CreateFloor(Floor floor)
        {
            _context.Floors.Add(floor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFloor), new { id = floor.FloorId }, floor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFloor(Guid id, Floor floor)
        {
            if (id != floor.FloorId)
            {
                return BadRequest();
            }

            _context.Entry(floor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FloorExists(id))
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
        public async Task<IActionResult> DeleteFloor(Guid id)
        {
            var floor = await _context.Floors.FindAsync(id);
            if (floor == null)
            {
                return NotFound();
            }

            floor.IsDeleted = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FloorExists(Guid id)
        {
            return _context.Floors.Any(e => e.FloorId == id);
        }
    }
}
