using Application.Appointments;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Persistence;

namespace API.Controllers
{
    /// <summary>
    /// Controller for managing appointments.
    /// </summary>
    public class AppointmentsController : BaseApiController
    {
        private readonly ApplicationDbContext _context;

        public AppointmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        // GET: /api/appointments/all-appointments
        [HttpGet("all-appointments")]
        public async Task<IActionResult> GetAppoitments()
        {
            return HandleResult(await Mediator.Send(new AppointmentsList.Query()));
        }

        /// <inheritdoc />
        // GET: /api/appointments/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppoitment(Guid id)
        {
            return HandleResult(await Mediator.Send(new AppointmentDetails.Query { Id = id }));
        }

        // POST: /api/appointments/create-new-appointment
        [HttpPost("create-new-appointment")]
        public async Task<IActionResult> CreateAppointment([FromBody] AppointmentCreate.Command command)
        {
            return HandleResult(await Mediator.Send(command));
        }


        /// <inheritdoc />
        // PUT: /api/appointments
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(Guid id, Appointment appointment)
        {
            return HandleResult(await Mediator.Send(new AppointmentUpdate.Command { Id = id, Appointment = appointment }));
        }


        /// <inheritdoc />
        // DELETE: /api/appointments/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(Guid id)
        {
            return HandleResult(await Mediator.Send(new AppointmentDelete.Command { Id = id }));
        }
    }
}
