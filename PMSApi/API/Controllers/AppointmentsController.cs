using Application.Appointments;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
    /// <summary>
    /// Controller for managing appointments.
    /// </summary>
    public class AppointmentsController : BaseApiController
    {
        private readonly ApplicationDbContext _context;
        private readonly INotificationService _notificationService;

        public AppointmentsController(ApplicationDbContext context, INotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;
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

        [HttpGet("confirm/{id}")]
        public async Task<IActionResult> ConfirmAppointment(Guid id)
        {
            var appointment = await _context.Appointments
                .Include(a => a.Patient)
                    .ThenInclude(p => p.User) 
                .Include(a => a.Doctor)
                    .ThenInclude(d => d.User) 
                .FirstOrDefaultAsync(a => a.AppointmentId == id);

            if (appointment == null)
            {
                return NotFound("Appointment not found");
            }

            var patientUserName = appointment.Patient?.User?.DisplayName ?? "Patient";
            var doctorUserName = appointment.Doctor?.User?.DisplayName ?? "Doctor";

            await _context.SaveChangesAsync();

            var message = $"Patient {patientUserName} confirmed the appointment with Dr. {doctorUserName}.";
            await _notificationService.SendNotificationAsync("Appointment confirmed", message);

            return Ok(message);
        }



    }
}
