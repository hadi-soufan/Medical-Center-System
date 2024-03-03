using Application.Core;
using Application.Patients;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Doctors
{
    public class DoctorList
    {
        public class Query : IRequest<Result<List<DoctorDto>>> { }

        public class Handler : IRequestHandler<Query, Result<List<DoctorDto>>>
        {
            private readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Result<List<DoctorDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var doctors = await _context.Doctors
                    .Include(d => d.User)
                    .Select(d => new DoctorDto
                    {
                        DoctorId = d.DoctorId,
                        DisplayName = d.User.DisplayName,
                        Username = d.User.UserName,
                        FatherName = d.User.FatherName,
                        MotherName = d.User.MotherName,
                        Nationality = d.User.Nationality,
                        Education = d.User.Education,
                        MaritalStatus = d.User.MaritalStatus,
                        City = d.User.City,
                        State = d.User.State,
                        Email = d.User.Email,
                        PhoneNumber = d.User.PhoneNumber,
                        DateOfBirth = d.User.DateOfBirth,
                        Gender = d.User.Gender,
                        BloodType = d.User.BloodType,
                        Address = d.User.Address,
                        Occupation = d.User.Occupation,
                        InsuranceId = d.User.InsuranceId,
                        DoctorLicenseId = d.DoctorLicenseId
                    })
                    .ToListAsync();

                return Result<List<DoctorDto>>.Success(doctors);
            }
        }

    }
}
