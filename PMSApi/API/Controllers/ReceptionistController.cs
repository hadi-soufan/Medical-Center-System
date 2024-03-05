using Application.Appointments;
using Application.Receptionist;
using Application.Receptionists;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Controller responsible for managing receptionist entities.
    /// </summary>
    public class ReceptionistController : BaseApiController
    {
        /// <inheritdoc />
        // GET: /api/receptionist/all-receptionist
        [HttpGet("all-receptionist")]
        public async Task<IActionResult> GetReceptionist()
        {
            return HandleResult(await Mediator.Send(new ReceptionistList.Query()));
        }

        /// <inheritdoc />
        // GET: /api/receptionist/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleReceptionist(Guid id)
        {
            return HandleResult(await Mediator.Send(new ReceptionistDetails.Query { Id = id }));
        }

        /// <inheritdoc />
        // PUT: /api/receptionist/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> ReceptionistUpdate(ReceptionistDto ReceptionistDto, Guid id)
        {
            return HandleResult(await Mediator.Send(new ReceptionistUpdate.Command { ReceptionistDto = ReceptionistDto, Id = id }));
        }

        /// <inheritdoc />
        // DELETE: /api/receptionist/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReceptionist(Guid id)
        {
            return HandleResult(await Mediator.Send(new ReceptionistDelete.Command { Id = id }));
        }
    }
}
