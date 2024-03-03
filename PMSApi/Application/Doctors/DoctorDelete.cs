using Application.Core;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Doctors
{
    public class DoctorDelete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid DoctorId { get; set; }
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
                var doctor = await _context.Doctors.FindAsync(request.DoctorId);

                if (doctor is null) return Result<Unit>.Failure("Doctor not found");

                _context.Doctors.Remove(doctor);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Doctor was not deleted");

                return Result<Unit>.Success(Unit.Value);

            }
        }
    }
}
