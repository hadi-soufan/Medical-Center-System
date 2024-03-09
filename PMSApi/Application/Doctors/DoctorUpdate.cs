using Application.Core;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Doctors
{
    /// <summary>
    /// Provides functionality to update a doctor.
    /// </summary>
    public class DoctorUpdate
    {
        /// <summary>
        /// Command to update a doctor.
        /// </summary>
        public class Command : IRequest<Result<Unit>>
        {
            public Guid DoctorId { get; set; }
            public DoctorDto DoctorDto { get; set; }
        }

        /// <summary>
        /// Validator for the DoctorUpdate command.
        /// </summary>
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator() 
            {
                RuleFor(x => x.DoctorDto).SetValidator(new DoctorValidators());
            }
        }

        /// <summary>
        /// Handler to process the DoctorUpdate command.
        /// </summary>
        public class Handler(ApplicationDbContext context) : IRequestHandler<Command, Result<Unit>>
        {
            /// <summary>
            /// Handles the DoctorUpdate command.
            /// </summary>
            /// <param name="request">The update command.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A result indicating success or failure of the update operation.</returns>
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var doctor = await context.Doctors
                    .Include(d => d.User)
                    .Where(d => !d.IsDeleted)
                    .FirstOrDefaultAsync(d => d.DoctorId == request.DoctorId, cancellationToken);

                    if (doctor is null) return Result<Unit>.Failure("Doctor not found");

                    doctor.User.Email = request.DoctorDto.Email;
                    doctor.User.PhoneNumber = request.DoctorDto.PhoneNumber;
                    doctor.User.Address = request.DoctorDto.Address;
                    doctor.User.City = request.DoctorDto.City;
                    doctor.User.State = request.DoctorDto.State;
                    doctor.User.Occupation = request.DoctorDto.Occupation;

                    context.Entry(doctor).State = EntityState.Modified;

                    doctor.UpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow);

                    var result = await context.SaveChangesAsync(cancellationToken) > 0;

                    if (!result) return Result<Unit>.Failure("Failed to update doctor");

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
