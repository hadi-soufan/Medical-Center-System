using MediatR;
using Persistence;
using Domain.Entities;
using Application.Appoitments;
using FluentValidation;
using Application.MedicalHistoreis;

namespace Application.MedicalHistories
{
    /// <summary>
    /// Represents a command to create a new medical history.
    /// </summary>
    public class MedicalHistoryCreate
    {
        /// <summary>
        /// Represents the command to create a new medical history.
        /// </summary>
        public class Command : IRequest
        {
            /// <summary>
            /// Gets or sets the medical history to be created.
            /// </summary>
            public MedicalHistory MedicalHistory { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.MedicalHistory).SetValidator(new MedicalHistoryValidators());
            }
        }

        /// <summary>
        /// Represents the handler for the <see cref="Command"/> to create a new medical history.
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
            /// Handles the command to create a new medical history.
            /// </summary>
            /// <param name="request">The command representing the medical history to be created.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A task representing the asynchronous operation. The task result is a <see cref="Unit"/>.</returns>
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                request.MedicalHistory.CreatedAt = DateTime.UtcNow;
                request.MedicalHistory.UpdatedAt = DateTime.UtcNow;

                _context.MedicalHistories.Add(request.MedicalHistory);

                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }

}
