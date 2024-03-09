using Application.Core;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Receptionist
{
    /// <summary>
    /// Represents a query to retrieve details of a receptionist.
    /// </summary>
    public class ReceptionistDetails
    {
        /// <summary>
        /// Represents the query to retrieve details of a receptionist.
        /// </summary>
        public class Query : IRequest<Result<ReceptionistDto>>
        {
            public Guid Id { get; set; }
        }

        /// <summary>
        /// Handles the receptionist details query.
        /// </summary>
        public class Handler(ApplicationDbContext context, IMapper mapper) : IRequestHandler<Query, Result<ReceptionistDto>>
        {
            /// <summary>
            /// Handles the receptionist details query.
            /// </summary>
            /// <param name="request">The receptionist details query.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A result containing the receptionist details.</returns>
            public async Task<Result<ReceptionistDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var receptionist = await context.Receptionists
                    .Include(r => r.User)
                    .Where(r => !r.IsDeleted)
                    .FirstOrDefaultAsync(r => r.ReceptionistId == request.Id, cancellationToken);

                    if (receptionist is null) return Result<ReceptionistDto>.Failure("Receptionist not found");

                    var receptionistDto = mapper.Map<ReceptionistDto>(receptionist);

                    return Result<ReceptionistDto>.Success(receptionistDto);
                }
                catch (Exception ex)
                {
                    return Result<ReceptionistDto>.Failure(ex.Message);
                }
            }
        }

    }
}
