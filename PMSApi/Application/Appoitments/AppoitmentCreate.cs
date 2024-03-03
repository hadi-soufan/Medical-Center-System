using Application.Appoitments;
using Application.Core;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Appointments
{
    public class AppointmentCreate
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Appointment Appointment { get; set; }
            public string PatientUsername { get; set; }
            public string DoctorUsername { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Appointment).SetValidator(new AppointmentValidator());
            }
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
                try
                {
                    var user = await _context.Users
                        .Include(u => u.Patients)
                        .SingleOrDefaultAsync(u => u.UserName == request.PatientUsername, cancellationToken);

                    if (user is null) return Result<Unit>.Failure("User not found");

                    var patient = user.Patients.FirstOrDefault();

                    if (patient is null) return Result<Unit>.Failure("Patient not found for the user");

                    var doctor = await _context.Doctors
                        .Include(d => d.User) 
                        .FirstOrDefaultAsync(d => d.User.UserName == request.DoctorUsername, cancellationToken);

                    if (doctor is null) return Result<Unit>.Failure("Doctor not found");

                    request.Appointment.PatientId = patient.PatientId;
                    request.Appointment.DoctorId = doctor.DoctorId;

                    request.Appointment.CreatedAt = DateTime.UtcNow;
                    request.Appointment.UpdatedAt = DateTime.UtcNow;

                    _context.Appointments.Add(request.Appointment);
                    var result = await _context.SaveChangesAsync(cancellationToken) > 0;

                    if (!result) return Result<Unit>.Failure("Failed to create new appointment");


                    return Result<Unit>.Success(Unit.Value);
                }
                catch (Exception ex)
                {
                    return Result<Unit>.Failure(ex.Message);
                }
            }
        }
    }
}
