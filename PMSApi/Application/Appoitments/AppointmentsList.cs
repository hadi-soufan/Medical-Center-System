using Application.Appoitments;
using Application.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Appointments
{
    public class AppointmentsList
        {
            public class Query : IRequest<Result<List<AppointmentDto>>>
            {

            }

        public class Handler : IRequestHandler<Query, Result<List<AppointmentDto>>>
        {
            private readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Result<List<AppointmentDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var appointments = await _context.Appointments
                    .Include(a => a.Patient)
                    .Select(a => new AppointmentDto
                    {
                        AppointmentId = a.AppointmentId,
                        AppointmentDate = a.AppointmentDate,
                        AppointmentStatus = a.AppointmentStatus,
                        AppointmentType = a.AppointmentType,
                        Notes = a.Notes,
                        PatientUsername = a.Patient.User.UserName,
                        DoctorUsername = a.Doctor.User.UserName
                    })
                    .ToListAsync(cancellationToken);

                return Result<List<AppointmentDto>>.Success(appointments);
            }
        }
    }
    }
