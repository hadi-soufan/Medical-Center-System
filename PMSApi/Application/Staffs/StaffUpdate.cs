using Application.Core;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Staffs
{
    /// <summary>
    /// Represents a command to update a staff member.
    /// </summary>
    public class StaffUpdate
    {
        /// <summary>
        /// Represents the command to update a staff member.
        /// </summary>
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id{ get; set; }
            public StaffDto StaffDto { get; set; }
        }

        /// <summary>
        /// Validator for the <see cref="Command"/> class.
        /// </summary>
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator() 
            {
                RuleFor(x => x.StaffDto).SetValidator(new StaffValidator());
            }
        }

        /// <summary>
        /// Handles the staff update command.
        /// </summary>
        public class Handler(ApplicationDbContext context) : IRequestHandler<Command, Result<Unit>>
        {
            /// <summary>
            /// Handles the staff update command.
            /// </summary>
            /// <param name="request">The staff update command.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A result indicating the outcome of the command.</returns>
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var staff = await context.Staffs
                    .Include(s => s.User)
                    .Where(s => !s.IsDeleted)
                    .FirstOrDefaultAsync(s => s.StaffId == request.Id, cancellationToken);

                    if (staff is null) return Result<Unit>.Failure("Staff not found");

                    staff.User.Email = request.StaffDto.Email;
                    staff.User.PhoneNumber = request.StaffDto.PhoneNumber;
                    staff.User.Address = request.StaffDto.Address;
                    staff.User.City = request.StaffDto.City;
                    staff.User.State = request.StaffDto.State;
                    staff.User.Occupation = request.StaffDto.Occupation;

                    context.Entry(staff).State = EntityState.Modified;

                    staff.UpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow);

                    var result = await context.SaveChangesAsync(cancellationToken) > 0;

                    if (!result) return Result<Unit>.Failure("Failed to update staff");

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
