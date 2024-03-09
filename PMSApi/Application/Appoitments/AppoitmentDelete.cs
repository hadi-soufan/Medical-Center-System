using Application.Core;
using MediatR;
using Persistence;

namespace Application.Appointments
{
    /// <summary>
    /// Provides functionality to delete an appointment.
    /// </summary>
    public class AppointmentDelete
    {

        /// <summary>
        /// Command to delete an appointment.
        /// </summary>
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
        }

        /// <summary>
        /// Handler to process the AppointmentDelete command.
        /// </summary>
        public class Handler(ApplicationDbContext context) : IRequestHandler<Command, Result<Unit>>
        {

            /// <summary>
            /// Handles the AppointmentDelete command.
            /// </summary>
            /// <param name="request">The delete command.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A result indicating success or failure of the delete operation.</returns>
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var appointment = await context.Appointments.FindAsync(new object[] { request.Id }, cancellationToken: cancellationToken);

                    if (appointment is null) return null;

                    appointment.IsCancelled = true;

                    var result = await context.SaveChangesAsync(cancellationToken) > 0;

                    if (!result) return Result<Unit>.Failure("Failed to Delete the Appointment");

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