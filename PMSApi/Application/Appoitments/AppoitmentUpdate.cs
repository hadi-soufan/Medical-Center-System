using AutoMapper;
using Domain;
using MediatR;

using Persistence;

/// <summary>
/// Represents a command to update an appointment.
/// </summary>
public class AppointmentUpdate
{
    /// <summary>
    /// Represents the command to update an appointment.
    /// </summary>
    public class Command : IRequest
    {
        /// <summary>
        /// Gets or sets the appointment to be updated.
        /// </summary>
        public Appointment Appointment { get; set; }
    }

    /// <summary>
    /// Represents the handler for the <see cref="Command"/> to update an appointment.
    /// </summary>
    public class Handler : IRequestHandler<Command>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="Handler"/> class with the specified application database context and mapper.
        /// </summary>
        /// <param name="context">The application database context.</param>
        /// <param name="mapper">The mapper.</param>
        public Handler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the command to update an appointment.
        /// </summary>
        /// <param name="request">The command representing the request to update an appointment.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing the asynchronous operation. The task result is the completion status.</returns>
        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            request.Appointment.UpdatedAt = DateTime.UtcNow;

            var appointment = await _context.Appointments.FindAsync(request.Appointment.AppointmentId);

            _mapper.Map(request.Appointment, appointment);

            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
