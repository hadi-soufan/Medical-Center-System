using Application.Core;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Patients
{
    public class PatientList
    {
        public class Query : IRequest<Result<List<PatientDto>>> { }

        public class Handler : IRequestHandler<Query, Result<List<PatientDto>>>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(ApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<List<PatientDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var patients = await _context.Patients
                    .Include(p => p.User)
                    .Select(p => new PatientDto
                    {
                        PatientId = p.PatientId,
                        DisplayName = p.User.DisplayName,
                        Username = p.User.UserName,
                        Email = p.User.Email,
                        PhoneNumber = p.User.PhoneNumber,
                        DateOfBirth = p.User.DateOfBirth,
                        Gender = p.User.Gender,
                        BloodType = p.User.BloodType,
                        Address = p.User.Address,
                        Occupation = p.User.Occupation,
                        InsuranceId = p.User.InsuranceId
                    })
                    .ToListAsync();

                return Result<List<PatientDto>>.Success(patients);
            }

        }
    }
}
