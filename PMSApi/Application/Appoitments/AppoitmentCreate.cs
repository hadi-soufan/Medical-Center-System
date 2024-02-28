using Domain;
using MediatR;

using Persistence;

/// <summary>
/// Represents a command to create a new appointment.
/// </summary>
public class AppointmentCreate
{
    /// <summary>
    /// Represents the command to create a new appointment.
    /// </summary>
    public class Command : IRequest
    {
        /// <summary>
        /// Gets or sets the appointment to be created.
        /// </summary>
        public Appointment Appointment { get; set; }
    }

    /// <summary>
    /// Represents the handler for the <see cref="Command"/> to create a new appointment.
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
        /// Handles the command to create a new appointment.
        /// </summary>
        /// <param name="request">The command representing the request to create a new appointment.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing the asynchronous operation. The task result is the completion status.</returns>
        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            request.Appointment.CreatedAt = DateTime.UtcNow;
            request.Appointment.UpdatedAt = DateTime.UtcNow;

            _context.Appointments.Add(request.Appointment);

            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
