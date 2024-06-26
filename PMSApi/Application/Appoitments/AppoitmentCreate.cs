﻿using Application.Appoitments;
using Application.Core;
using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Net.Mail;

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
        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IAppointmentUpdateSender _appointmentUpdateSender;
            private readonly IEmailService _emailService;

            public Handler(IApplicationDbContext context, IAppointmentUpdateSender appointmentUpdateSender, IEmailService emailService)
            {
                _context = context;
                _appointmentUpdateSender = appointmentUpdateSender;
                _emailService = emailService;
            }

            public ApplicationDbContext DbContext { get; }
            public IAppointmentUpdateSender Object { get; }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var user = await _context.Users
                        .Include(u => u.Patients)
                        .Where(u => !u.IsCancelled)
                        .SingleOrDefaultAsync(u => u.UserName == request.PatientUsername, cancellationToken);

                    if (user is null) return Result<Unit>.Failure("User not found");

                    var patient = user.Patients.FirstOrDefault();
                    if (patient is null) return Result<Unit>.Failure("Patient not found");

                    var doctor = await _context.Doctors
                        .Include(d => d.User)
                        .FirstOrDefaultAsync(d => d.User.UserName == request.DoctorUsername, cancellationToken);

                    if (doctor is null) return Result<Unit>.Failure("Doctor not found");

                    request.Appointment.PatientId = patient.PatientId;
                    request.Appointment.DoctorId = doctor.DoctorId;

                    _context.Appointments.Add(request.Appointment);
                    var result = await _context.SaveChangesAsync(cancellationToken) > 0;

                    if (!result) return Result<Unit>.Failure("Failed to create new appointment");

                    await _appointmentUpdateSender.SendAppointmentUpdate("New appointment created");

                    var emailSubject = "Your Appointment is Set";
                    var appointmentDate = request.Appointment.AppointmentDateStart.ToString("MM/dd/yyyy 'at' hh:mm:ss tt");
                    var emailBody = $"Dear {patient.User.DisplayName},<br/><br/>Your appointment with Dr. {doctor.User.DisplayName} is set for {appointmentDate}.<br/><br/>Thank you.";
                    await _emailService.SendEmailAsync(user.Email, emailSubject, emailBody);


                    return Result<Unit>.Success(Unit.Value);
                }
                catch (SmtpException smtpEx)
                {
                    return Result<Unit>.Failure($"Email sending failed: {smtpEx.Message}");
                }
                catch (Exception ex)
                {
                    return Result<Unit>.Failure(ex.Message);
                }
            }
        }
    }
}
