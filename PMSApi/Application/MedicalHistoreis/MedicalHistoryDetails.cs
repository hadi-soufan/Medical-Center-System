using Application.Core;
using Domain.Entities;
using MediatR;
using Persistence;

namespace Application.MedicalHistoreis
{
    /// <summary>
    /// Represents a query to retrieve details of a medical history.
    /// </summary>
    public class MedicalHistoryDetails
    {
        /// <summary>
        /// Represents the query to retrieve details of a medical history.
        /// </summary>
        public class Query : IRequest<Result<MedicalHistory>>
        {
            public Guid Id { get; set; }
        }

        /// <summary>
        /// Handler for processing the MedicalHistoryDetails query.
        /// </summary>
        public class Handler(ApplicationDbContext context) : IRequestHandler<Query, Result<MedicalHistory>>
        {
            /// <summary>
            /// Handles the MedicalHistoryDetails query.
            /// </summary>
            /// <param name="request">The query request.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A result containing the medical history details.</returns>
            public async Task<Result<MedicalHistory>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var medicalHistory = await context.MedicalHistories.FindAsync(new object[] { request.Id }, cancellationToken: cancellationToken);

                    return Result<MedicalHistory>.Success(medicalHistory);
                }
                catch (Exception ex)
                {
                    return Result<MedicalHistory>.Failure(ex.Message);
                }
            }
        }
    }

}
