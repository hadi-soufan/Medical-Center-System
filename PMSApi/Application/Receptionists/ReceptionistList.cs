using Application.Core;
using Application.Receptionist;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Receptionists
{
    /// <summary>
    /// Represents a query to retrieve a list of receptionists.
    /// </summary>
    public class ReceptionistList
    {
        /// <summary>
        /// Represents the query to retrieve a list of receptionists.
        /// </summary>
        public class Query : IRequest<Result<List<ReceptionistDto>>> { }

        /// <summary>
        /// Handles the receptionist list query.
        /// </summary>
        public class Hanlder(ApplicationDbContext context, IMapper mapper) : IRequestHandler<Query, Result<List<ReceptionistDto>>>
        {
            /// <summary>
            /// Handles the receptionist list query.
            /// </summary>
            /// <param name="request">The receptionist list query.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A result containing the list of receptionists.</returns>
            public async Task<Result<List<ReceptionistDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var receptionists = await context.Receptionists
                    .Include(r => r.User)
                    .Where(r => !r.IsDeleted)
                    .ToListAsync(cancellationToken);

                    if (receptionists is null || receptionists.Count is 0) return Result<List<ReceptionistDto>>.Failure("No Receptionists found");

                    var receptionistDtos = receptionists.Select(r => mapper.Map<ReceptionistDto>(r)).ToList();

                    return Result<List<ReceptionistDto>>.Success(receptionistDtos);
                }
                catch (Exception ex)
                {
                    return Result<List<ReceptionistDto>>.Failure(ex.Message);
                }
            }
        }

    }
}
