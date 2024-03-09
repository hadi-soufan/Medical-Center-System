using Application.Core;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Accountants
{
    /// <summary>
    /// Provides functionality to list all accountants.
    /// </summary>
    public class AccoutantList
    {
        /// <summary>
        /// Query to retrieve a list of accountants.
        /// </summary>
        public class Query : IRequest<Result<List<AccoutantDto>>> { }


        /// <summary>
        /// Handler to process the query and return a list of accountants.
        /// </summary>
        public class Handler(ApplicationDbContext context, IMapper mapper) : IRequestHandler<Query, Result<List<AccoutantDto>>>
        {

            /// <summary>
            /// Handles the query to retrieve a list of accountants.
            /// </summary>
            /// <param name="request">The query request.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A result containing a list of accountant DTOs.</returns>
            public async Task<Result<List<AccoutantDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var accountants = await context.Accountants
                    .Include(a => a.User)
                    .Where(a => !a.IsDeleted)
                    .ToListAsync(cancellationToken);

                    var accountantDtos = mapper.Map<List<Accountant>, List<AccoutantDto>>(accountants);

                    return Result<List<AccoutantDto>>.Success(accountantDtos);
                }
                catch (Exception ex)
                {
                    return Result<List<AccoutantDto>>.Failure(ex.Message);
                }
            }
        }
    }
}
