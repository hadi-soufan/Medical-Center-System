using Application.Core;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Nurses
{
    /// <summary>
    /// Represents a query to retrieve a list of nurses.
    /// </summary>
    public class NurseList
    {
        /// <summary>
        /// Represents the query to retrieve a list of nurses.
        /// </summary>
        public class Query : IRequest<Result<List<NurseDto>>> { }

        /// <summary>
        /// Handler for processing the NurseList query.
        /// </summary>
        public class Handler(ApplicationDbContext context, IMapper mapper) : IRequestHandler<Query, Result<List<NurseDto>>> 
        {
            /// <summary>
            /// Handles the NurseList query.
            /// </summary>
            /// <param name="request">The query request.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A result containing the list of nurse DTOs.</returns>
            public async Task<Result<List<NurseDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var nurses = await context.Nurses
                   .Include(n => n.User)
                   .Where(n => !n.IsDeleted)
                   .ToListAsync(cancellationToken);

                    if (nurses.Count is 0) return Result<List<NurseDto>>.Failure("No Nurse Data Found");

                    var nurseDtos = mapper.Map<List<Nurse>, List<NurseDto>>(nurses);

                    return Result<List<NurseDto>>.Success(nurseDtos);
                }
                catch (Exception ex)
                {
                    return Result<List<NurseDto>>.Failure(ex.Message);
                }
            }
        }
    }
}
