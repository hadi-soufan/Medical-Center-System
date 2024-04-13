using Microsoft.AspNetCore.SignalR;

namespace API.Hubs
{
    /// <summary>
    /// Represents a hub for managing appointments.
    /// </summary>
    public class AppointmentHub : Hub
    {
        /// <summary>
        /// Sends an appointment update message to all connected clients.
        /// </summary>
        /// <param name="message">The message to send.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task SendAppointmentUpdate(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }

        /// <summary>
        /// Notifies all connected clients about the deletion of an appointment.
        /// </summary>
        /// <param name="message">The message to send.</param>
        /// <param name="appointmentId">The ID of the deleted appointment.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task NotifyAppointmentDeletion(string message, Guid appointmentId)
        {
            await Clients.All.SendAsync("AppointmentDeleted", appointmentId);
        }
    }
}
