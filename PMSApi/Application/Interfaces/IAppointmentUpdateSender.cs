using Domain.Entities;

namespace Application.Interfaces
{
    /// <summary>
    /// Represents an interface for sending appointment updates and notifications.
    /// </summary>
    public interface IAppointmentUpdateSender
    {
        /// <summary>
        /// Sends an appointment update message.
        /// </summary>
        /// <param name="message">The message to send.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task SendAppointmentUpdate(string message);

        /// <summary>
        /// Notifies about an appointment deletion.
        /// </summary>
        /// <param name="message">The message to send.</param>
        /// <param name="appointmentId">The ID of the appointment being deleted.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task NotifyAppointmentDeletion(string message, Guid appointmentId);

        /// <summary>
        /// Notifies about an appointment update.
        /// </summary>
        /// <param name="message">The message to send.</param>
        /// <param name="appointment">The updated appointment.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task NotifyAppointmentUpdated(string message, Appointment appointment);
    }
}
