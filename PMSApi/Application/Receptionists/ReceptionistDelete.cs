using Application.Core;
using MediatR;
using Persistence;

namespace Application.Receptionist
{
    /// <summary>
    /// Represents a command to delete a receptionist.
    /// </summary>
    public class ReceptionistDelete
    {
        /// <summary>
        /// Represents the command to delete a receptionist.
        /// </summary>
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
        }

        /// <summary>
        /// Handles the receptionist delete command.
        /// </summary>
        public class Handler(ApplicationDbContext context) : IRequestHandler<Command, Result<Unit>>
        {
            /// <summary>
            /// Handles the receptionist delete command.
            /// </summary>
            /// <param name="request">The receptionist delete command.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A result indicating the outcome of the operation.</returns>
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var receptionist = await context.Receptionists.FindAsync(new object[] { request.Id }, cancellationToken: cancellationToken);

                    if (receptionist is null) return Result<Unit>.Failure("Receptionist not found");

                    receptionist.IsDeleted = true;

                    var result = await context.SaveChangesAsync(cancellationToken) > 0;

                    if (!result) return Result<Unit>.Failure("Receptionist is not Deleted");

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
