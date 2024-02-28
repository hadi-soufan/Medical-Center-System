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
    public class Command : IRequest
    {
        /// <summary>
        /// Gets or sets the ID of the appointment to be deleted.
        /// </summary>
        public Guid Id { get; set; }
    }

    /// <summary>
    /// Represents the handler for the <see cref="Command"/> to delete an appointment.
    /// </summary>
    public class Handler : IRequestHandler<Command>
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
        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var appointment = await _context.Appointments.FindAsync(request.Id);

            _context.Remove(appointment);

            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
