using Application.Core;
using Application.MedicalHistoreis;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.MedicalHistories
{
    /// <summary>
    /// Represents a query to retrieve a list of medical histories.
    /// </summary>
    public class MedicalHistoryList
    {
        /// <summary>
        /// Represents the query to retrieve a list of medical histories.
        /// </summary>
        public class Query : IRequest<Result<List<MedicalHistoryDto>>> { }

        /// <summary>
        /// Handler for processing the MedicalHistoryList query.
        /// </summary>
        public class Handler(ApplicationDbContext context, IMapper mapper) : IRequestHandler<Query, Result<List<MedicalHistoryDto>>>
        {
            /// <summary>
            /// Handles the MedicalHistoryList query.
            /// </summary>
            /// <param name="request">The query request.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A result containing the list of medical history DTOs.</returns>
            public async Task<Result<List<MedicalHistoryDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var medicalHistories = await context.MedicalHistories
                    .Include(mh => mh.Patient)
                    .ThenInclude(p => p.User)
                    .Where(mh => !mh.IsDeleted)
                    .ToListAsync(cancellationToken);

                    if (medicalHistories.Count is 0) return Result<List<MedicalHistoryDto>>.Failure("No Medical History Data Found");

                    var medicalHistoryDtos = mapper.Map<List<MedicalHistory>, List<MedicalHistoryDto>>(medicalHistories);

                    return Result<List<MedicalHistoryDto>>.Success(medicalHistoryDtos);

                }
                catch (Exception ex)
                {
                    return Result<List<MedicalHistoryDto>>.Failure(ex.Message);
                }
            }

        }
    }
}
