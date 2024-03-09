using Application.Appoitments;
using Application.Core;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Appointments
{
    /// <summary>
    /// Provides functionality to create a new appointment.
    /// </summary>
    public class AppointmentCreate
    {
        /// <summary>
        /// Command to create a new appointment.
        /// </summary>
        public class Command : IRequest<Result<Unit>>
        {
            public Appointment Appointment { get; set; }
            public string PatientUsername { get; set; }
            public string DoctorUsername { get; set; }
        }

        /// <summary>
        /// Validator for the AppointmentCreate command.
        /// </summary>
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Appointment).SetValidator(new AppointmentValidator());
            }
        }

        /// <summary>
        /// Handler to process the AppointmentCreate command.
        /// </summary>
        public class Handler(ApplicationDbContext context) : IRequestHandler<Command, Result<Unit>>
        {
            /// <summary>
            /// Handles the AppointmentCreate command.
            /// </summary>
            /// <param name="request">The create command.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A result indicating success or failure of the create operation.</returns>
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var user = await context.Users
                        .Include(u => u.Patients)
                        .Where(u => !u.IsCancelled)
                        .SingleOrDefaultAsync(u => u.UserName == request.PatientUsername, cancellationToken);

                    if (user is null) return Result<Unit>.Failure("User not found");

                    var patient = user.Patients.FirstOrDefault();

                    if (patient is null) return Result<Unit>.Failure("Patient not found");

                    var doctor = await context.Doctors
                        .Include(d => d.User) 
                        .FirstOrDefaultAsync(d => d.User.UserName == request.DoctorUsername, cancellationToken);

                    if (doctor is null) return Result<Unit>.Failure("Doctor not found");

                    request.Appointment.PatientId = patient.PatientId;
                    request.Appointment.DoctorId = doctor.DoctorId;

                    context.Appointments.Add(request.Appointment);
                    var result = await context.SaveChangesAsync(cancellationToken) > 0;

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
