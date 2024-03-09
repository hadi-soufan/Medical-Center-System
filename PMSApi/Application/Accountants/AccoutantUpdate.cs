using Application.Core;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Accountants
{
    /// <summary>
    /// Provides functionality to update an accountant.
    /// </summary>
    public class AccoutantUpdate
    {
        /// <summary>
        /// Command to update an accountant.
        /// </summary>
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
            public AccoutantDto AccoutantDto { get; set; }
        }

        /// <summary>
        /// Validator for the AccoutantUpdate command.
        /// </summary>
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.AccoutantDto).SetValidator(new AccountantValidator());
            }
        }

        /// <summary>
        /// Handler to process the AccoutantUpdate command.
        /// </summary>
        public class Handler(ApplicationDbContext context) : IRequestHandler<Command, Result<Unit>>
        {

            /// <summary>
            /// Handles the AccoutantUpdate command.
            /// </summary>
            /// <param name="request">The update command.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A result indicating success or failure of the update operation.</returns>
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var accountant = await context.Accountants
                    .Include(a => a.User)
                    .FirstOrDefaultAsync(a => request.Id == a.AccountantId, cancellationToken);

                    if (accountant is null) return Result<Unit>.Failure("Accountant not Found");

                    accountant.User.Email = request.AccoutantDto.Email;
                    accountant.User.PhoneNumber = request.AccoutantDto.PhoneNumber;
                    accountant.User.Address = request.AccoutantDto.Address;
                    accountant.User.City = request.AccoutantDto.City;
                    accountant.User.State = request.AccoutantDto.State;
                    accountant.User.Occupation = request.AccoutantDto.Occupation;

                    accountant.UpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow);

                    var result = await context.SaveChangesAsync(cancellationToken) > 0;

                    if (!result) return Result<Unit>.Failure("Failed to update accountant");

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
