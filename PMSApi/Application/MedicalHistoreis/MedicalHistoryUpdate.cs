using Application.Core;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Persistence;

namespace Application.MedicalHistories
{
    /// <summary>
    /// Represents a command to update a medical history.
    /// </summary>
    public class MedicalHistoryUpdate
    {
        /// <summary>
        /// Represents the command to update a medical history.
        /// </summary>
        public class Command : IRequest<Result<Unit>>
        {
            public MedicalHistory MedicalHistory { get; set; }
        }

        /// <summary>
        /// Handler for processing the MedicalHistoryUpdate command.
        /// </summary>
        public class Handler(ApplicationDbContext context, IMapper mapper) : IRequestHandler<Command, Result<Unit>>
        {
            /// <summary>
            /// Handles the MedicalHistoryUpdate command.
            /// </summary>
            /// <param name="request">The command request.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A result indicating success or failure of the update operation.</returns>
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var medicalHistory = await context.MedicalHistories.FindAsync(new object[] { request.MedicalHistory.MedicalHistoryId }, cancellationToken: cancellationToken);

                    if (medicalHistory is null) return null;

                    mapper.Map(request.MedicalHistory, medicalHistory);

                    medicalHistory.UpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow);

                    var result = await context.SaveChangesAsync(cancellationToken) > 0;

                    if (!result) return Result<Unit>.Failure("Failed to Updated Medical History");

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