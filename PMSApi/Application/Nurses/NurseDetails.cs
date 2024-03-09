using Application.Core;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Nurses
{
    /// <summary>
    /// Represents a query to retrieve details of a nurse.
    /// </summary>
    public class NurseDetails
    {
        /// <summary>
        /// Represents the query to retrieve details of a nurse.
        /// </summary>
        public class Query : IRequest<Result<NurseDto>>
        {
            public Guid Id { get; set; }
        }

        /// <summary>
        /// Handler for processing the NurseDetails query.
        /// </summary>
        public class Handler(ApplicationDbContext context, IMapper mapper) : IRequestHandler<Query, Result<NurseDto>>
        {
            /// <summary>
            /// Handles the NurseDetails query.
            /// </summary>
            /// <param name="request">The query request.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A result indicating success or failure with the nurse details.</returns>
            public async Task<Result<NurseDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var nurse = await context.Nurses
                   .Include(n => n.User)
                   .Where(n => !n.IsDeleted)
                   .FirstOrDefaultAsync(n => n.NurseId == request.Id, cancellationToken);

                    if (nurse is null) return Result<NurseDto>.Failure("Nurse not found");

                    var nurseDto = mapper.Map<Nurse, NurseDto>(nurse);

                    return Result<NurseDto>.Success(nurseDto);
                }

                catch (Exception ex)
                {
                    return Result<NurseDto>.Failure(ex.Message);
                }
            }
        }
    }
}
