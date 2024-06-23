using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BuildingController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Building>>> GetBuildings()
        {
            return await _context.Buildings.Include(b => b.Center).Where(b => !b.IsDeleted).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Building>> GetBuilding(Guid id)
        {
            var building = await _context.Buildings.Include(b => b.Center).FirstOrDefaultAsync(b => b.BuildingId == id);

            if (building == null)
            {
                return NotFound();
            }

            return building;
        }

        [HttpPost]
        public async Task<ActionResult<Building>> CreateBuilding(Building building)
        {
            _context.Buildings.Add(building);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBuilding), new { id = building.BuildingId }, building);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBuilding(Guid id, Building building)
        {
            if (id != building.BuildingId)
            {
                return BadRequest();
            }

            _context.Entry(building).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuildingExists(id))
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
        public async Task<IActionResult> DeleteBuilding(Guid id)
        {
            var building = await _context.Buildings.FindAsync(id);
            if (building == null)
            {
                return NotFound();
            }

            building.IsDeleted = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BuildingExists(Guid id)
        {
            return _context.Buildings.Any(e => e.BuildingId == id);
        }
    }
}
