using Application.Core;
using MediatR;
using Persistence;

namespace Application.MedicalHistories
{
    /// <summary>
    /// Represents a command to delete a medical history.
    /// </summary>
    public class MedicalHistoryDelete
    {
        /// <summary>
        /// Represents the command to delete a medical history.
        /// </summary>
        public class Command : IRequest<Result<Unit>>
        {
            /// <summary>
            /// Gets or sets the ID of the medical history to be deleted.
            /// </summary>
            public Guid Id { get; set; }
        }

        /// <summary>
        /// Represents the handler for the <see cref="Command"/> to delete a medical history.
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
            /// Handles the command to delete a medical history.
            /// </summary>
            /// <param name="request">The command representing the ID of the medical history to be deleted.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A task representing the asynchronous operation. The task result is a <see cref="Unit"/>.</returns>
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var medicalHistory = await _context.MedicalHistories.FindAsync(request.Id);

                if (medicalHistory is null) return null;

                _context.Remove(medicalHistory);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to Delete Medical History");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }

}
