using Application.Core;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Accountants
{
    /// <summary>
    /// Handles retrieving details of an accountant.
    /// </summary>
    public class AccoutantDetails
    {
        /// <summary>
        /// Represents a query to retrieve details of an accountant.
        /// </summary>
        public class Query : IRequest<Result<AccoutantDto>>
        {
            public Guid Id { get; set; }
        }

        /// <summary>
        /// Handles the execution of the AccoutantDetails query.
        /// </summary>
        public class Handler(ApplicationDbContext context, IMapper mapper) : IRequestHandler<Query, Result<AccoutantDto>>
        {
            /// <summary>
            /// Handles the execution of the AccoutantDetails query.
            /// </summary>
            /// <param name="request">The query request.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A Result containing the accountant details if found; otherwise, a failure result.</returns>
            public async Task<Result<AccoutantDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var accountant = await context.Accountants
                    .Include(a => a.User)
                    .Where(a => !a.IsDeleted)
                    .FirstOrDefaultAsync(a => a.AccountantId == request.Id, cancellationToken);

                    if (accountant is null) return Result<AccoutantDto>.Failure("Accountant not found");

                    var accountantDto = mapper.Map<Accountant, AccoutantDto>(accountant);

                    return Result<AccoutantDto>.Success(accountantDto);
                }
                catch (Exception ex)
                {
                    return Result<AccoutantDto>.Failure(ex.Message);
                }
            }
        }
    }
}
