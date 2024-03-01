using Application.Core;
using MediatR;

using Persistence;

/// <summary>
/// Represents a command to delete an appointment.
/// </summary>
public class AppointmentDelete
{
    /// <summary>
    /// Represents the command to delete an appointment.
    /// </summary>
    public class Command : IRequest<Result<Unit>>
    {
        /// <summary>
        /// Gets or sets the ID of the appointment to be deleted.
        /// </summary>
        public Guid Id { get; set; }
    }

    /// <summary>
    /// Represents the handler for the <see cref="Command"/> to delete an appointment.
    /// </summary>
    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="Handler"/> class with the specified application database context.
        /// </summary>
        /// <param name="context">The application database context.</param>
        public Handler(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Handles the command to delete an appointment.
        /// </summary>
        /// <param name="request">The command representing the request to delete an appointment.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing the asynchronous operation. The task result is the completion status.</returns>
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var appointment = await _context.Appointments.FindAsync(request.Id);

            if (appointment is null) return null;

            _context.Remove(appointment);

            var result = await _context.SaveChangesAsync() > 0;

            if (!result) return Result<Unit>.Failure("Failed to Delete the Appointment");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
