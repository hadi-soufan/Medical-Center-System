using Application.Core;
using AutoMapper;
using Domain.Entities;
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
    public class Command : IRequest<Result<Unit>>
    {
        /// <summary>
        /// Gets or sets the appointment to be updated.
        /// </summary>
        public Appointment Appointment { get; set; }
    }

    /// <summary>
    /// Represents the handler for the <see cref="Command"/> to update an appointment.
    /// </summary>
    public class Handler : IRequestHandler<Command, Result<Unit>>
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
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            request.Appointment.UpdatedAt = DateTime.UtcNow;

            var appointment = await _context.Appointments.FindAsync(request.Appointment.AppointmentId);

            if (appointment is null) return null;

            _mapper.Map(request.Appointment, appointment);

            var result = await _context.SaveChangesAsync() > 0;

            if (!result) return Result<Unit>.Failure("Failed to Update the Appointment");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
