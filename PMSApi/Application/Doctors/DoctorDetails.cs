using Application.Appoitments;
using Application.Core;
using Domain.Entities;
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
    public class DoctorDetails
    {
        public class Query: IRequest<Result<DoctorDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<DoctorDto>>
        {
            private readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Result<DoctorDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var doctor = await _context.Doctors
                    .Include(d => d.User)
                    .FirstOrDefaultAsync(d => d.DoctorId == request.Id, cancellationToken);

                if (doctor == null)
                {
                    return Result<DoctorDto>.Failure("Doctor not found");
                }

                var doctorDto = new DoctorDto
                {
                    DoctorId = doctor.DoctorId,
                    DisplayName = doctor.User.DisplayName,
                    Email = doctor.User.Email,
                    Username = doctor.User.UserName,
                    PhoneNumber = doctor.User.PhoneNumber,
                    FatherName = doctor.User.FatherName,
                    MotherName = doctor.User.MotherName,
                    DateOfBirth = doctor.User.DateOfBirth,
                    Nationality = doctor.User.Nationality,
                    Education = doctor.User.Education,
                    Gender = doctor.User.Gender,
                    MaritalStatus = doctor.User.MaritalStatus,
                    BloodType = doctor.User.BloodType,
                    Address = doctor.User.Address,
                    City = doctor.User.City,
                    ZipCode = doctor.User.ZipCode,
                    State = doctor.User.State,
                    Occupation = doctor.User.Occupation,
                    InsuranceId = doctor.User.InsuranceId,
                    DoctorLicenseId = doctor.DoctorLicenseId
                };

                return Result<DoctorDto>.Success(doctorDto);
            }

        }
    }
}
