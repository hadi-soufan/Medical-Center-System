using Application.Core;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Doctors
{
    /// <summary>
    /// Provides functionality to retrieve a list of doctors.
    /// </summary>
    public class DoctorList
    {
        /// <summary>
        /// Query to retrieve a list of doctors.
        /// </summary>
        public class Query : IRequest<Result<PageList<DoctorDto>>>
        {
            public PagingParams Params { get; set; }
        }

        /// <summary>
        /// Handler to process the DoctorList query.
        /// </summary>
        public class Handler(ApplicationDbContext context, IMapper mapper) : IRequestHandler<Query, Result<PageList<DoctorDto>>>
        {
            /// <summary>
            /// Handles the DoctorList query.
            /// </summary>
            /// <param name="request">The query request.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A result containing the list of doctors.</returns>
            public async Task<Result<PageList<DoctorDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var doctors = await context.Doctors
                        .Include(d => d.User)
                        .Include(d => d.Appointments.Where(a => !a.IsCancelled))
                        .Where(d => !d.IsDeleted)
                        .ToListAsync();

                    var doctorDtos = mapper.Map<List<DoctorDto>>(doctors);

                    var query = await PageList<DoctorDto>.CreateAsync(doctorDtos, request.Params.PageNumber, request.Params.PageSize);

                    return Result<PageList<DoctorDto>>.Success(query);
                }
                catch (Exception ex)
                {
                    return Result<PageList<DoctorDto>>.Failure(ex.Message);
                }
            }

        }

    }
}
