using Application.Photos;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
    /// <summary>
    /// Controller responsible for handling photo-related operations.
    /// </summary>
    public class PhotoController : BaseApiController
    {
        private readonly ApplicationDbContext _context;

        public PhotoController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        // GET: api/photo
        [HttpGet]
        public async Task<ActionResult<List<PatientPhoto>>> GetPhotos()
        {
            return await _context.PatientPhotos.ToListAsync();
        }

        /// <inheritdoc />
        // GET: api/photo/id
        [HttpGet("user/{id}")]
        public async Task<ActionResult<List<PatientPhoto>>> GetUsersPhotos(string id)
        {
            return HandleResult(await Mediator.Send(new ListUsersPhoto.Query { UserId = id }));
        }

        /// <inheritdoc />
        // GET: api/photo/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientPhoto>> GetPhoto(string id)
        {
            try
            {
                var photo = await _context.PatientPhotos.FindAsync(id);

                if (photo is null) return NotFound();

                return photo;
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching photo", ex);
            }
        }

        /// <inheritdoc />
        // Post: api/photo
        [HttpPost]
        public async Task<IActionResult> AddPhotos([FromForm] Add.Command commnad)
        {
            return HandleResult(await Mediator.Send(commnad));
        }

        /// <inheritdoc />
        // PUT: api/photo
        [HttpPut("{id}")]
        public async Task<IActionResult> EditPhoto(string id, PatientPhoto photo)
        {
            photo.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command { Photo = photo }));
        }

        /// <inheritdoc />
        // Post: api/photo/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
        }
    }
}
