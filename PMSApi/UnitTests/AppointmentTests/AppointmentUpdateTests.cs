using Application.Appointments;
using Application.Appoitments;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UnitTests.AppointmentTests
{
    public class AppointmentUpdateTests
    {
        private readonly Mock<IApplicationDbContext> _mockContext;
        private readonly Mock<IAppointmentUpdateSender> _mockAppointmentUpdateSender;
        private readonly AppointmentValidator _validator;

        public AppointmentUpdateTests()
        {
            _mockContext = new Mock<IApplicationDbContext>();
            _mockAppointmentUpdateSender = new Mock<IAppointmentUpdateSender>();
            _validator = new AppointmentValidator();
        }


        [Fact]
        public async Task Handle_ValidId_UpdatesAppointmentDetails()
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

            var mockAppointmentUpdateSender = new Mock<IAppointmentUpdateSender>();
            mockAppointmentUpdateSender.Setup(x => x.NotifyAppointmentUpdated(It.IsAny<string>(), It.IsAny<Appointment>()))
                .Returns(Task.CompletedTask);

            var updatedAppointment = new Appointment
            {
                AppointmentDateStart = DateTime.Now.AddDays(1),
                AppointmentDateEnd = DateTime.Now.AddDays(2),
                AppointmentStatus = "Confirmed",
                AppointmentType = "Online",
                Notes = "Updated appointment details"
            };

            var command = new AppointmentUpdate.Command { Id = validAppointmentId, Appointment = updatedAppointment };
            var handler = new AppointmentUpdate.Handler(context, mockAppointmentUpdateSender.Object, _validator);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            var updatedAppointmentInDb = await context.Appointments.FindAsync(validAppointmentId);
            Assert.Equal(updatedAppointment.AppointmentDateStart, updatedAppointmentInDb?.AppointmentDateStart);
            Assert.Equal(updatedAppointment.AppointmentDateEnd, updatedAppointmentInDb?.AppointmentDateEnd);
            Assert.Equal(updatedAppointment.AppointmentStatus, updatedAppointmentInDb?.AppointmentStatus);
            Assert.Equal(updatedAppointment.AppointmentType, updatedAppointmentInDb?.AppointmentType);
            Assert.Equal(updatedAppointment.Notes, updatedAppointmentInDb?.Notes);
        }

        [Fact]
        public async Task Handle_EmptyAppointmentType_ReturnsFailureResult()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var context = new ApplicationDbContext(options);

            Guid appointmentId = Guid.NewGuid();
            var appointment = new Appointment
            {
                AppointmentId = appointmentId,
                AppointmentDateStart = DateTime.Now.AddDays(1),
                AppointmentDateEnd = DateTime.Now.AddDays(2),
                AppointmentStatus = "Confirmed",
                AppointmentType = "Online",
                Notes = "Updated appointment details"
            };

            context.Appointments.Add(appointment);
            await context.SaveChangesAsync();

            var command = new AppointmentUpdate.Command
            {
                Id = appointmentId,
                Appointment = new Appointment 
                {
                    AppointmentId = appointmentId,
                    AppointmentDateStart = DateTime.Now.AddDays(1),
                    AppointmentDateEnd = DateTime.Now.AddDays(2),
                    AppointmentStatus = "Confirmed",
                    AppointmentType = string.Empty,
                    Notes = "Updated appointment details",
                }
            };
            var handler = new AppointmentUpdate.Handler(context, _mockAppointmentUpdateSender.Object, _validator);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("'Appointment Type' must not be empty.", result.Error);
        }

        [Fact]
        public async Task Handle_EmptyAppointmentDateEnd_ReturnsFailureResult()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var context = new ApplicationDbContext(options);

            Guid appointmentId = Guid.NewGuid();
            var appointment = new Appointment
            {
                AppointmentId = appointmentId,
                AppointmentDateStart = DateTime.Now.AddDays(1),
                AppointmentDateEnd = DateTime.Now.AddDays(2),
                AppointmentStatus = "Confirmed",
                AppointmentType = "Online",
                Notes = "Updated appointment details"
            };

            context.Appointments.Add(appointment);
            await context.SaveChangesAsync();

            var command = new AppointmentUpdate.Command
            {
                Id = appointmentId,
                Appointment = new Appointment
                {
                    AppointmentId = appointmentId,
                    AppointmentDateStart = DateTime.Now.AddDays(1),
                    AppointmentDateEnd = DateTime.MinValue,
                    AppointmentStatus = "Confirmed",
                    AppointmentType = "On Site",
                    Notes = "Updated appointment details",
                }
            };
            var handler = new AppointmentUpdate.Handler(context, _mockAppointmentUpdateSender.Object, _validator);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("'Appointment Date End' must not be empty.", result.Error);
        }

        [Fact]
        public async Task Handle_EmptyAppointmentDateStart_ReturnsFailureResult()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var context = new ApplicationDbContext(options);

            Guid appointmentId = Guid.NewGuid();
            var appointment = new Appointment
            {
                AppointmentId = appointmentId,
                AppointmentDateStart = DateTime.Now.AddDays(1),
                AppointmentDateEnd = DateTime.Now.AddDays(2),
                AppointmentStatus = "Confirmed",
                AppointmentType = "Online",
                Notes = "Updated appointment details"
            };

            context.Appointments.Add(appointment);
            await context.SaveChangesAsync();

            var command = new AppointmentUpdate.Command
            {
                Id = appointmentId,
                Appointment = new Appointment
                {
                    AppointmentId = appointmentId,
                    AppointmentDateStart = DateTime.MinValue,
                    AppointmentDateEnd = DateTime.Now.AddDays(2),
                    AppointmentStatus = "Confirmed",
                    AppointmentType = "On Site",
                    Notes = "Updated appointment details",
                }
            };
            var handler = new AppointmentUpdate.Handler(context, _mockAppointmentUpdateSender.Object, _validator);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("'Appointment Date Start' must not be empty.", result.Error);
        }

    }
}
