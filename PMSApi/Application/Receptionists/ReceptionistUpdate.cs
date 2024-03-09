using Application.Core;
using Application.Receptionists;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Receptionist
{
    /// <summary>
    /// Represents a command to update a receptionist.
    /// </summary>
    public class ReceptionistUpdate
    {
        /// <summary>
        /// Represents the command to update a receptionist.
        /// </summary>
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
            public ReceptionistDto ReceptionistDto { get; set; }
        }

        /// <summary>
        /// Represents the validator for the receptionist update command.
        /// </summary>
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator() 
            {
                RuleFor(x => x.ReceptionistDto).SetValidator(new ReceptionistValidator());
            }   
        }

        /// <summary>
        /// Handles the receptionist update command.
        /// </summary>
        public class Handler(ApplicationDbContext context) : IRequestHandler<Command, Result<Unit>>
        {
            /// <summary>
            /// Handles the receptionist update command.
            /// </summary>
            /// <param name="request">The receptionist update command.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A result indicating the outcome of the operation.</returns>
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var receptionist = await context.Receptionists
                    .Include(r => r.User)
                    .Where(r => !r.IsDeleted)
                    .FirstOrDefaultAsync(r => r.ReceptionistId == request.Id, cancellationToken);

                    if (receptionist is null) return Result<Unit>.Failure("Receptionist not found");

                    receptionist.User.Email = request.ReceptionistDto.Email;
                    receptionist.User.PhoneNumber = request.ReceptionistDto.PhoneNumber;
                    receptionist.User.Address = request.ReceptionistDto.Address;
                    receptionist.User.City = request.ReceptionistDto.City;
                    receptionist.User.State = request.ReceptionistDto.State;
                    receptionist.User.Occupation = request.ReceptionistDto.Occupation;

                    context.Entry(receptionist).State = EntityState.Modified;

                    receptionist.UpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow);

                    var result = await context.SaveChangesAsync(cancellationToken) > 0;

                    if (!result) return Result<Unit>.Failure("Failed to update receptionist");

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
