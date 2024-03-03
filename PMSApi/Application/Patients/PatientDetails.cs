using Application.Core;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Patients
{
    public class PatientDetails
    {
        public class Query : IRequest<Result<PatientDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PatientDto>>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(ApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<PatientDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var patient = await _context.Patients
                    .FirstOrDefaultAsync(p => p.PatientId == request.Id);

                if (patient == null)
                    return Result<PatientDto>.Failure("Patient not found");

                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Id == patient.UserId);

                if (user == null)
                    return Result<PatientDto>.Failure("User not found");

                var patientDto = new PatientDto
                {
                    PatientId = patient.PatientId,
                    DisplayName = patient.User.DisplayName,
                    Username = patient.User.UserName,
                    Email = patient.User.Email,
                    PhoneNumber = patient.User.PhoneNumber,
                    DateOfBirth = patient.User.DateOfBirth,
                    Gender = patient.User.Gender,
                    BloodType = patient.User.BloodType,
                    Address = patient.User.Address,
                    Occupation = patient.User.Occupation,
                    InsuranceId = patient.User.InsuranceId
                };

                return Result<PatientDto>.Success(patientDto);
            }

        }
    }
}
