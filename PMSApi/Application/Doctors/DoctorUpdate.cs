using Application.Core;
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
    public class DoctorUpdate
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid DoctorId { get; set; }
            public DoctorDto DoctorDto { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var doctor = await _context.Doctors
                    .Include(d => d.User)
                    .FirstOrDefaultAsync(d => d.DoctorId == request.DoctorId, cancellationToken);

                if (doctor is null) return Result<Unit>.Failure("Doctor not found");


                doctor.User.Email = request.DoctorDto.Email;
                doctor.User.PhoneNumber = request.DoctorDto.PhoneNumber;
                doctor.User.Address = request.DoctorDto.Address;
                doctor.User.City = request.DoctorDto.City;
                doctor.User.State = request.DoctorDto.State;
                doctor.User.Occupation = request.DoctorDto.Occupation;

                _context.Entry(doctor).State = EntityState.Modified;

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to update doctor");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
