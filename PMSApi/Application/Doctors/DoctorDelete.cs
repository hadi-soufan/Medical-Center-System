using Application.Core;
using MediatR;
using Persistence;

namespace Application.Doctors
{
    /// <summary>
    /// Provides functionality to delete a doctor.
    /// </summary>
    public class DoctorDelete
    {
        /// <summary>
        /// Command to delete a doctor.
        /// </summary>
        public class Command : IRequest<Result<Unit>>
        {
            public Guid DoctorId { get; set; }
        }

        /// <summary>
        /// Handler to process the DoctorDelete command.
        /// </summary>
        public class Handler(ApplicationDbContext context) : IRequestHandler<Command, Result<Unit>>
        {
            /// <summary>
            /// Handles the DoctorDelete command.
            /// </summary>
            /// <param name="request">The delete command.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A result indicating success or failure of the delete operation.</returns>
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var doctor = await context.Doctors.FindAsync(new object[] { request.DoctorId }, cancellationToken: cancellationToken);

                    if (doctor is null) return Result<Unit>.Failure("Doctor not found");

                    doctor.IsDeleted = true;

                    var result = await context.SaveChangesAsync(cancellationToken) > 0;

                    if (!result) return Result<Unit>.Failure("Doctor was not deleted");

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
