using Application.Appointments;
using Application.Core;
using Application.Interfaces;
using Domain.Entities;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using Persistence;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Appointments
{
    public class AppointmentCreateTests
    {
        [Fact]
        public async Task Handle_ValidCommand_ShouldCreateAppointment()
        {
            // Arrange
            var appointment = new Appointment
            {
                AppointmentId = Guid.NewGuid(),
                AppointmentDateStart = DateTime.Now,
                AppointmentDateEnd = DateTime.Now,
                AppointmentStatus = "Pending",
                AppointmentType = "OnSite",
                Notes = "some notes"
            };


            var patientUsername = "johnds";
            var doctorUsername = "hannah";

            var user = new AppUser
            {
                UserName = patientUsername,
                Patients = new[] { new Patient { PatientId = Guid.NewGuid() } }
            };

            var doctor = new Doctor
            {
                User = new AppUser { UserName = doctorUsername },
                DoctorId = Guid.NewGuid()
            };

            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using (var context = new ApplicationDbContext(dbContextOptions))
            {
                context.Users.Add(user);
                context.Doctors.Add(doctor);
                await context.SaveChangesAsync();
            }

            var dbContext = new ApplicationDbContext(dbContextOptions);
            var appointmentUpdateSenderMock = new Mock<IAppointmentUpdateSender>();

            var command = new AppointmentCreate.Command
            {
                Appointment = appointment,
                PatientUsername = patientUsername,
                DoctorUsername = doctorUsername
            };

            var handler = new AppointmentCreate.Handler(dbContext, appointmentUpdateSenderMock.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeOfType<Result<Unit>>();
            result.IsSuccess.Should().BeTrue();

            using (var context = new ApplicationDbContext(dbContextOptions))
            {
                var createdAppointment = context.Appointments.FirstOrDefault(a => a.AppointmentId == appointment.AppointmentId);
                createdAppointment.Should().NotBeNull();
                createdAppointment?.PatientId.Should().Be(user.Patients.First()?.PatientId.ToString() ?? string.Empty);
                createdAppointment?.DoctorId.Should().Be(doctor.DoctorId);
            }
        }
    }
}
