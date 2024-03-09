using Application.Core;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Patients
{
    /// <summary>
    /// Represents a query to retrieve details of a patient.
    /// </summary>
    public class PatientDetails
    {
        /// <summary>
        /// Represents the query to retrieve details of a patient.
        /// </summary>
        public class Query : IRequest<Result<PatientDto>>
        {
            public Guid Id { get; set; }
        }

        /// <summary>
        /// Handles the patient details query.
        /// </summary>
        public class Handler(ApplicationDbContext context, IMapper mapper) : IRequestHandler<Query, Result<PatientDto>>
        {
            /// <summary>
            /// Handles the patient details query.
            /// </summary>
            /// <param name="request">The patient details query.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A result containing the patient details.</returns>
            public async Task<Result<PatientDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var patient = await context.Patients
                    .Include(p => p.User)
                    .Where(p => !p.IsDeleted)
                    .FirstOrDefaultAsync(p => p.PatientId == request.Id, cancellationToken);

                    if (patient is null) return Result<PatientDto>.Failure("Patient not found");

                    var patientDto = mapper.Map<Patient, PatientDto>(patient);

                    return Result<PatientDto>.Success(patientDto);
                }
                catch (Exception ex)
                {
                    return Result<PatientDto>.Failure(ex.Message);
                }
            }

        }
    }
}
