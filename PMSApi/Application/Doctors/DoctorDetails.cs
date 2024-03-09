using Application.Core;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Doctors
{
    /// <summary>
    /// Provides functionality to retrieve details of a doctor.
    /// </summary>
    public class DoctorDetails
    {
        /// <summary>
        /// Query to retrieve details of a doctor.
        /// </summary>
        public class Query: IRequest<Result<DoctorDto>>
        {
            public Guid Id { get; set; }
        }

        /// <summary>
        /// Handler to process the DoctorDetails query.
        /// </summary>
        public class Handler(ApplicationDbContext context, IMapper mapper) : IRequestHandler<Query, Result<DoctorDto>>
        {
            /// <summary>
            /// Handles the DoctorDetails query.
            /// </summary>
            /// <param name="request">The details query.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A result containing details of the doctor.</returns>
            public async Task<Result<DoctorDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var doctor = await context.Doctors
                    .Include(d => d.User)
                    .Where(d => !d.IsDeleted)
                    .FirstOrDefaultAsync(d => d.DoctorId == request.Id, cancellationToken);

                    if (doctor is null) return Result<DoctorDto>.Failure("Doctor not found");

                    var doctorDto = mapper.Map<Doctor, DoctorDto>(doctor);

                    return Result<DoctorDto>.Success(doctorDto);
                }
                catch (Exception ex)
                {
                    return Result<DoctorDto>.Failure(ex.Message);
                }
            }

        }
    }
}
