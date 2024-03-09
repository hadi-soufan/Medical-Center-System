using Application.Core;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Staffs
{
    /// <summary>
    /// Represents a query to retrieve details of a staff member.
    /// </summary>
    public class StaffDetails
    {
        /// <summary>
        /// Represents the query to retrieve details of a staff member.
        /// </summary>
        public class Query : IRequest<Result<StaffDto>>
        {
            public Guid Id { get; set; }
        }
        /// <summary>
        /// Handles the request to retrieve details of a staff member.
        /// </summary>
        public class Handler(ApplicationDbContext context, IMapper mapper) : IRequestHandler<Query, Result<StaffDto>>
        {
            /// <summary>
            /// Handles the request to retrieve details of a staff member.
            /// </summary>
            /// <param name="request">The request to retrieve details of a staff member.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A result containing the staff member details.</returns>
            public async Task<Result<StaffDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var staff = await context.Staffs
                    .Include(s => s.User)
                    .FirstOrDefaultAsync(s => s.StaffId == request.Id, cancellationToken);

                    if (staff is null)
                        return Result<StaffDto>.Failure("Staff not found");

                    var staffDto = mapper.Map<Staff, StaffDto>(staff);

                    return Result<StaffDto>.Success(staffDto);
                }
                catch (Exception ex)
                {
                    return Result<StaffDto>.Failure(ex.Message);
                }
            }
        }
    }
}
