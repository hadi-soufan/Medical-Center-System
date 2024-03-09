using Application.Core;
using MediatR;
using Persistence;

namespace Application.Nurses
{
    /// <summary>
    /// Represents a command to delete a nurse.
    /// </summary>
    public class NurseDelete
    {
        /// <summary>
        /// Represents the command to delete a nurse.
        /// </summary>
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
        }

        /// <summary>
        /// Handler for processing the NurseDelete command.
        /// </summary>
        public class Handler(ApplicationDbContext context) : IRequestHandler<Command, Result<Unit>>
        {
            /// <summary>
            /// Handles the NurseDelete command.
            /// </summary>
            /// <param name="request">The command request.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A result indicating success or failure of the delete operation.</returns>
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var nurse = await context.Nurses.FindAsync(new object[] { request.Id }, cancellationToken: cancellationToken);

                    if (nurse is null) return Result<Unit>.Failure("Nurse Not Found");

                    nurse.IsDeleted = true;

                    var result = await context.SaveChangesAsync(cancellationToken) > 0;

                    if (!result) return Result<Unit>.Failure("Nurse is not Deleted");

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
