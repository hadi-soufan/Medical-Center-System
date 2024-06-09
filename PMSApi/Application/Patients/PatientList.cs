using Application.Core;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Patients
{
    /// <summary>
    /// Represents a query to retrieve a list of patients.
    /// </summary>
    public class PatientList
    {
        /// <summary>
        /// Represents the query to retrieve a list of patients.
        /// </summary>
        public class Query : IRequest<Result<List<PatientDto>>> { }

        /// <summary>
        /// Handles the patient list query.
        /// </summary>
        public class Handler(ApplicationDbContext context, IMapper mapper) : IRequestHandler<Query, Result<List<PatientDto>>>
        {
            /// <summary>
            /// Handles the patient list query.
            /// </summary>
            /// <param name="request">The patient list query.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A result containing the list of patients.</returns>
            public async Task<Result<List<PatientDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var patients = await context.Patients
                    .Include(p => p.User)
                    .Include(p => p.MedicalHistory)
                    .Where(p => !p.IsDeleted)
                    .ToListAsync(cancellationToken);

                    if (patients.Count is 0) return Result<List<PatientDto>>.Failure("No patient data found");

                    var patientDtos = mapper.Map<List<Patient>, List<PatientDto>>(patients);

                    return Result<List<PatientDto>>.Success(patientDtos);
                }
                catch (Exception ex)
                {
                    return Result<List<PatientDto>>.Failure(ex.Message);
                }
            }

        }
    }
}
