using Application.Core;
using MediatR;
using Persistence;

namespace Application.Patients
{
    /// <summary>
    /// Represents a command to delete a patient.
    /// </summary>  
    public class PatientDelete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
        }

        /// <summary>
        /// Handles the patient delete command.
        /// </summary>
        public class Handler(ApplicationDbContext context) : IRequestHandler<Command, Result<Unit>>
        {
            /// <summary>
            /// Handles the patient delete command.
            /// </summary>
            /// <param name="request">The delete patient command.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A result indicating whether the operation was successful.</returns>
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var patient = await context.Patients.FindAsync(new object[] { request.Id }, cancellationToken: cancellationToken);

                    if (patient is null) return null;

                    patient.IsDeleted = true;

                    var result = await context.SaveChangesAsync(cancellationToken) > 0;

                    if (!result) return Result<Unit>.Failure("Failed to Delete the Patient");

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
