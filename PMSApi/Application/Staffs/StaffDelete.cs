using Application.Core;
using MediatR;
using Persistence;

namespace Application.Staffs
{
    /// <summary>
    /// Represents a command to delete a staff member.
    /// </summary>
    public class StaffDelete
    {
        /// <summary>
        /// Represents the command to delete a staff member.
        /// </summary>
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
        }

        /// <summary>
        /// Handles the command to delete a staff member.
        /// </summary>
        public class Handler(ApplicationDbContext context) : IRequestHandler<Command, Result<Unit>>
        {
            /// <summary>
            /// Handles the command to delete a staff member.
            /// </summary>
            /// <param name="request">The delete staff command.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A result indicating success or failure.</returns>
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var staff = await context.Staffs.FindAsync(new object[] { request.Id }, cancellationToken: cancellationToken);

                    if (staff is null) return Result<Unit>.Failure("Staff Not Found");

                    staff.IsDeleted = true;

                    var result = await context.SaveChangesAsync(cancellationToken) > 0;

                    if (!result) return Result<Unit>.Failure("Staff Not Deleted");

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
