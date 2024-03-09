using Application.Core;
using MediatR;
using Persistence;

namespace Application.Accountants
{
    /// <summary>
    /// Defines a command to delete an accountant.
    /// </summary>
    public class AccoutantDelete
    {
        /// <summary>
        /// Represents a command to delete an accountant.
        /// </summary>
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
        }

        /// <summary>
        /// Represents a handler for the accountant delete command.
        /// </summary>
        public class Handler(ApplicationDbContext context) : IRequestHandler<Command, Result<Unit>>
        {
            /// <summary>
            /// Handles the accountant delete command.
            /// </summary>
            /// <param name="request">The delete command.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A result indicating success or failure of the delete operation.</returns>
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var accountant = await context.Accountants.FindAsync(new object[] { request.Id }, cancellationToken);

                    if (accountant is null) return Result<Unit>.Failure("Accoutant Not Found");

                    accountant.IsDeleted = true;

                    var result = await context.SaveChangesAsync(cancellationToken) > 0;

                    if (!result) return Result<Unit>.Failure("Accountant was not Deleted");

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
