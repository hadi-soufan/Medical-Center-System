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
        public class Command : IRequest
        {
            /// <summary>
            /// Gets or sets the ID of the medical history to be deleted.
            /// </summary>
            public Guid Id { get; set; }
        }

        /// <summary>
        /// Represents the handler for the <see cref="Command"/> to delete a medical history.
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
            /// Handles the command to delete a medical history.
            /// </summary>
            /// <param name="request">The command representing the ID of the medical history to be deleted.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A task representing the asynchronous operation. The task result is a <see cref="Unit"/>.</returns>
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var medicalHistory = await _context.MedicalHistories.FindAsync(request.Id);

                _context.Remove(medicalHistory);

                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }

}
