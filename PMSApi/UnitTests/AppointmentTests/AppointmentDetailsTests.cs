using Application.Appointments;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.AppointmentsTests
{
    public class AppointmentDetailsTests
    {
        private readonly Mock<IApplicationDbContext> _mockContext;
        private readonly AppointmentDetails.Handler _handler;

        public AppointmentDetailsTests()
        {
            _mockContext = new Mock<IApplicationDbContext>();
            _handler = new AppointmentDetails.Handler(_mockContext.Object);
        }

        [Fact]
        public async Task Handle_ValidId_ReturnsCorrectAppointmentDetails()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var context = new ApplicationDbContext(options);

            Guid validAppointmentId = Guid.Parse("1365bff0-6780-4153-8fd2-6bf9b8f75325");
            var appointment = new Appointment
            {
                AppointmentId = validAppointmentId,
                AppointmentDateStart = DateTime.Parse("2024-03-04T18:21:58.43"),
                AppointmentDateEnd = DateTime.Parse("0001-01-01T00:00:00"),
                AppointmentStatus = "Pending",
                AppointmentType = "OnSite",
                Notes = "Appointment with doctor zayn and patient saeinna",
                Patient = new Patient { User = new AppUser { UserName = "seinna1" } },
                Doctor = new Doctor { User = new AppUser { UserName = "zayn" } }
            };

            context.Appointments.Add(appointment);
            await context.SaveChangesAsync();

            var query = new AppointmentDetails.Query { Id = validAppointmentId };
            var handler = new AppointmentDetails.Handler(context);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(validAppointmentId, result.Value.AppointmentId);
            Assert.Equal(appointment.AppointmentDateStart, result.Value.AppointmentDateStart);
            Assert.Equal(appointment.AppointmentDateEnd, result.Value.AppointmentDateEnd);
            Assert.Equal(appointment.AppointmentStatus, result.Value.AppointmentStatus);
            Assert.Equal(appointment.AppointmentType, result.Value.AppointmentType);
            Assert.Equal(appointment.Notes, result.Value.Notes);
            Assert.Equal(appointment.Patient.User.UserName, result.Value.PatientUsername);
            Assert.Equal(appointment.Doctor.User.UserName, result.Value.DoctorUsername);
        }

        [Fact]
        public async Task Handle_InvalidId_ReturnsFailureResult()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var context = new ApplicationDbContext(options);

            Guid invalidAppointmentId = Guid.NewGuid();
            var query = new AppointmentDetails.Query { Id = invalidAppointmentId };
            var handler = new AppointmentDetails.Handler(context);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Appointment not found", result.Error);
        }


    }
}
