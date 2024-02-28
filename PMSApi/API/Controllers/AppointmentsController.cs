using Application.Appoitments;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Controller for managing appointments.
    /// </summary>
    public class AppointmentsController : BaseApiController
    {

        /// <inheritdoc />
        // GET: /api/appointments
        [HttpGet]
        public async Task<ActionResult<List<Appointment>>> GetAppoitments()
        {
            return await Mediator.Send(new AppointmentsList.Query());
        }

        /// <inheritdoc />
        // GET: /api/appointments
        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetAppoitment(Guid id)
        {
            return await Mediator.Send(new AppointmentDetails.Query { Id = id });
        }

        /// <inheritdoc />
        // POST: /api/appointments
        [HttpPost]
        public async Task<IActionResult> CreateAppoitment(Appointment appointment)
        {
            return Ok(await Mediator.Send(new AppointmentCreate.Command { Appointment = appointment}));
        }

        /// <inheritdoc />
        // PUT: /api/appointments
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(Guid id, Appointment appointment)
        {
            appointment.AppointmentId = id;

            return Ok(await Mediator.Send(new AppointmentUpdate.Command { Appointment = appointment }));
        }

        /// <inheritdoc />
        // DELETE: /api/appointments
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(Guid id)
        {
            return Ok(await Mediator.Send(new AppointmentDelete.Command { Id = id }));
        }
    }
}
