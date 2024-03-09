using Application.Core;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Nurses
{
    /// <summary>
    /// Represents a command to update a nurse.
    /// </summary>
    public class NurseUpdate
    {
        /// <summary>
        /// Represents the command to update a nurse.
        /// </summary>  
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
            public NurseDto NurseDto { get; set; }
        }

        /// <summary>
        /// Validator for the NurseUpdate command.
        /// </summary>
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.NurseDto).SetValidator(new NurseValidators());
            }
        }
        /// <summary>
        /// Handler for processing the NurseUpdate command.
        /// </summary>
        public class Handler(ApplicationDbContext context) : IRequestHandler<Command, Result<Unit>>
        {
            /// <summary>
            /// Handles the NurseUpdate command.
            /// </summary>
            /// <param name="request">The command request.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A result indicating the success or failure of the operation.</returns>
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var nurse = await context.Nurses
                    .Include(n => n.User)
                    .Where(n => !n.IsDeleted)
                    .FirstOrDefaultAsync(n => n.NurseId == request.Id, cancellationToken);

                    if (nurse is null) return Result<Unit>.Failure("Nurse not found");

                    nurse.User.Email = request.NurseDto.Email;
                    nurse.User.PhoneNumber = request.NurseDto.PhoneNumber;
                    nurse.User.Address = request.NurseDto.Address;
                    nurse.User.City = request.NurseDto.City;
                    nurse.User.State = request.NurseDto.State;
                    nurse.User.Occupation = request.NurseDto.Occupation;

                    context.Entry(nurse).State = EntityState.Modified;

                    nurse.UpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow);

                    var result = await context.SaveChangesAsync(cancellationToken) > 0;

                    if (!result) return Result<Unit>.Failure("Failed to update nurse");

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
