using Application.Core;
using Domain.Entities;
using MediatR;
using Persistence;

namespace Application.Appointments
{
    /// <summary>
    /// Provides functionality to update an appointment.
    /// </summary>
    public class AppointmentUpdate
    {
        /// <summary>
        /// Command to update an appointment.
        /// </summary>
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
            public Appointment Appointment { get; set; }
        }

        /// <summary>
        /// Handler to process the AppointmentUpdate command.
        /// </summary>
        public class Handler(ApplicationDbContext context) : IRequestHandler<Command, Result<Unit>>
        {
            /// <summary>
            /// Handles the AppointmentUpdate command.
            /// </summary>
            /// <param name="request">The update command.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A result indicating success or failure of the update operation.</returns>
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var appointment = await context.Appointments.FindAsync(new object[] { request.Id }, cancellationToken);

                    if (appointment is null) return Result<Unit>.Failure("Appointment not found");

                    appointment.AppointmentDateStart = request.Appointment.AppointmentDateStart;
                    appointment.AppointmentDateEnd = request.Appointment.AppointmentDateEnd;
                    appointment.AppointmentStatus = request.Appointment.AppointmentStatus;
                    appointment.AppointmentType = request.Appointment.AppointmentType;
                    appointment.Notes = request.Appointment.Notes;

                    appointment.UpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow);

                    var result = await context.SaveChangesAsync(cancellationToken) > 0;

                    if (!result) return Result<Unit>.Failure("Failed to update appointment");

                    return Result<Unit>.Success(Unit.Value);
                }
                catch (Exception ex)
                {
                    return Result<Unit>.Failure(ex.Message);
                }
            }
        }
    }
}
