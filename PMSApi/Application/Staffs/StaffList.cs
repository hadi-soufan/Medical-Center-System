using Application.Core;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Staffs
{
    /// <summary>
    /// Represents a query to retrieve a list of staff members.
    /// </summary>
    public class StaffList
    {
        /// <summary>
        /// Represents the query to retrieve a list of staff members.
        /// </summary>
        public class Query : IRequest<Result<List<StaffDto>>> { }

        /// <summary>
        /// Handles the request to retrieve a list of staff members.
        /// </summary>
        public class Handler(ApplicationDbContext context, IMapper mapper) : IRequestHandler<Query, Result<List<StaffDto>>>
        {
            /// <summary>
            /// Handles the request to retrieve a list of staff members.
            /// </summary>
            /// <param name="request">The request to retrieve a list of staff members.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A result containing the list of staff members.</returns>
            public async Task<Result<List<StaffDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var staffs = await context.Staffs
                    .Include(s => s.User)
                    .Where(s => !s.IsDeleted)
                    .ToListAsync(cancellationToken);

                    if (staffs is null || staffs.Count is 0) return Result<List<StaffDto>>.Failure("No Staff members found");

                    var staffDtos = staffs.Select(s => mapper.Map<StaffDto>(s)).ToList();

                    return Result<List<StaffDto>>.Success(staffDtos);
                }
                catch (Exception ex)
                {
                    return Result<List<StaffDto>>.Failure(ex.Message);
                }
            }
        }
    }
}
