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
            public Guid Id { get; set; }
        }

        /// <summary>
        /// Handler for processing the MedicalHistoryDelete command.
        /// </summary>
        public class Handler(ApplicationDbContext context) : IRequestHandler<Command, Result<Unit>>
        {
            /// <summary>
            /// Handles the MedicalHistoryDelete command.
            /// </summary>
            /// <param name="request">The command request.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A result indicating success or failure of the delete operation.</returns>
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var medicalHistory = await context.MedicalHistories.FindAsync(new object[] { request.Id }, cancellationToken: cancellationToken);

                    if (medicalHistory is null) return null;

                    medicalHistory.IsDeleted = true;

                    var result = await context.SaveChangesAsync(cancellationToken) > 0;

                    if (!result) return Result<Unit>.Failure("Failed to Delete Medical History");

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
