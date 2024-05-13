using Application.Appointments;
using Application.Core;
using Application.Interfaces;
using Domain.Entities;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Moq;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Appointments
{
    public class AppointmentDeleteTests
    {
        [Fact]
        public async Task Handle_ValidId_ReturnsSuccessResult()
        {
            // Arrange
            var appointmentId = Guid.NewGuid();
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using (var context = new ApplicationDbContext(dbContextOptions))
            {
                context.Appointments.Add(new Appointment { AppointmentId = appointmentId });
                context.SaveChanges();
            }

            var appointmentUpdateSenderMock = new Mock<IAppointmentUpdateSender>();

            var handler = new AppointmentDelete.Handler(new ApplicationDbContext(dbContextOptions), appointmentUpdateSenderMock.Object);
            var command = new AppointmentDelete.Command { Id = appointmentId };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_InvalidId_ReturnsFailureResult()
        {
            // Arrange
            var appointmentId = Guid.NewGuid();
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using (var context = new ApplicationDbContext(dbContextOptions))
            {
                context.Appointments.Add(new Appointment { AppointmentId = Guid.NewGuid() });
                context.SaveChanges();
            }

            var appointmentUpdateSenderMock = new Mock<IAppointmentUpdateSender>();

            var handler = new AppointmentDelete.Handler(new ApplicationDbContext(dbContextOptions), appointmentUpdateSenderMock.Object);
            var command = new AppointmentDelete.Command { Id = appointmentId };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeFalse();

        }
    }
}
