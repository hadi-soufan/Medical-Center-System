using Application.Appoitments;
using Application.Core;
using Domain.Entities;
using FluentValidation;
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
    public class Command : IRequest<Result<Unit>>
    {
        /// <summary>
        /// Gets or sets the appointment to be created.
        /// </summary>
        public Appointment Appointment { get; set; }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Appointment).SetValidator(new AppointmentValidator());
        }
    }

    /// <summary>
    /// Represents the handler for the <see cref="Command"/> to create a new appointment.
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
        /// Handles the command to create a new appointment.
        /// </summary>
        /// <param name="request">The command representing the request to create a new appointment.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing the asynchronous operation. The task result is the completion status.</returns>
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            request.Appointment.CreatedAt = DateTime.UtcNow;
            request.Appointment.UpdatedAt = DateTime.UtcNow;

            _context.Appointments.Add(request.Appointment);

            var result = await _context.SaveChangesAsync() > 0;

            if (!result) return Result<Unit>.Failure("Failed to create new Appointment");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
