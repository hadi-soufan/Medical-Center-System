using API.Hubs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.SignalR;
using Persistence.Migrations;

namespace API.Services
{
    /// <summary>
    /// Class responsible for sending appointment update notifications.
    /// </summary>
    public class AppointmentUpdateSender : IAppointmentUpdateSender
    {
        private readonly IHubContext<AppointmentHub> _hubContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentUpdateSender"/> class.
        /// </summary>
        /// <param name="hubContext">The hub context used for sending notifications.</param>
        public AppointmentUpdateSender(IHubContext<AppointmentHub> hubContext)
        {
            _hubContext = hubContext;
        }

        /// <summary>
        /// Notifies all clients about the deletion of an appointment.
        /// </summary>
        /// <param name="message">The notification message.</param>
        /// <param name="appointmentId">The ID of the deleted appointment.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task NotifyAppointmentDeletion(string message, Guid appointmentId)
        {
            await _hubContext.Clients.All.SendAsync("AppointmentDeleted", appointmentId);
        }

        /// <summary>
        /// Notifies all clients about an updated appointment.
        /// </summary>
        /// <param name="message">The notification message.</param>
        /// <param name="appointment">The updated appointment.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task NotifyAppointmentUpdated(string message, Appointment appointment)
        {
            await _hubContext.Clients.All.SendAsync("AppointmentUpdated", appointment);
        }

        /// <summary>
        /// Sends an appointment update message to all clients.
        /// </summary>
        /// <param name="message">The update message.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task SendAppointmentUpdate(string message)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
