using MediatR;
using Persistence;
using Domain.Entities;
using FluentValidation;
using Application.MedicalHistoreis;
using Application.Core;
using Microsoft.EntityFrameworkCore;

namespace Application.MedicalHistories
{
    /// <summary>
    /// Represents a command to create a new medical history.
    /// </summary>
    public class MedicalHistoryCreate
    {
        /// <summary>
        /// Command to create a new medical history.
        /// </summary>
        public class Command : IRequest<Result<Unit>>
        {
            public MedicalHistory MedicalHistory { get; set; }
        
        }

        /// <summary>
        /// Validator for the MedicalHistoryCreate command.
        /// </summary>
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.MedicalHistory).SetValidator(new MedicalHistoryValidators());
            }
        }

        /// <summary>
        /// Handler for processing the MedicalHistoryCreate command.
        /// </summary>
        public class Handler(ApplicationDbContext context) : IRequestHandler<Command, Result<Unit>>
        {
            /// <summary>
            /// Handles the MedicalHistoryCreate command.
            /// </summary>
            /// <param name="request">The command request.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A result indicating success or failure of the create operation.</returns>
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var patient = await context.Patients
                    .Include(p => p.User)
                    .SingleOrDefaultAsync(p => p.PatientId == request.MedicalHistory.PatientId, cancellationToken);

                    if (patient is null) return Result<Unit>.Failure("Patient not found");

                    request.MedicalHistory.PatientName = patient.User.DisplayName;

                    context.MedicalHistories.Add(request.MedicalHistory);

                    var result = await context.SaveChangesAsync(cancellationToken) > 0;

                    if (!result) return Result<Unit>.Failure("Failed to create new medical history");

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
