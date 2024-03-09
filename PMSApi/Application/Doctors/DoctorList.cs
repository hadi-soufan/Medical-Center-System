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
        public class Query : IRequest<Result<List<DoctorDto>>> { }

        /// <summary>
        /// Handler to process the DoctorList query.
        /// </summary>
        public class Handler(ApplicationDbContext context, IMapper mapper) : IRequestHandler<Query, Result<List<DoctorDto>>>
        {
            /// <summary>
            /// Handles the DoctorList query.
            /// </summary>
            /// <param name="request">The query request.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A result containing the list of doctors.</returns>
            public async Task<Result<List<DoctorDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var doctors = await context.Doctors
                    .Include(d => d.User)
                    .Include(d => d.Appointments)
                    .Where(d => !d.IsDeleted)
                    .ToListAsync(cancellationToken);

                    if (doctors.Count is 0) return Result<List<DoctorDto>>.Failure("No Doctors data");

                    var doctorDtos = mapper.Map<List<DoctorDto>>(doctors);

                    return Result<List<DoctorDto>>.Success(doctorDtos);
                }
                catch (Exception ex)
                {
                    return Result<List<DoctorDto>>.Failure(ex.Message);
                }
            }
        }

    }
}
