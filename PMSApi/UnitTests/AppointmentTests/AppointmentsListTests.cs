using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Appointments;
using Application.Appoitments;
using Application.Core;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using Persistence;
using Xunit;

namespace Application.UnitTests.Appointments
{
    public class AppointmentsListTests
    {
        private readonly Mock<IApplicationDbContext> _mockContext;
        private readonly AppointmentsList.Handler _handler;

        public AppointmentsListTests()
        {
            _mockContext = new Mock<IApplicationDbContext>();
            _handler = new AppointmentsList.Handler(_mockContext.Object, new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfiles());
            }).CreateMapper());
        }

        [Fact]
        public async Task Handle_ValidQuery_ReturnsListOfAppointments()
        {
            // Arrange
            var appointments = new List<Appointment>
    {
        new Appointment { AppointmentId = Guid.NewGuid(), Patient = new Patient(), Doctor = new Doctor(), IsCancelled = false },
        new Appointment { AppointmentId = Guid.NewGuid(), Patient = new Patient(), Doctor = new Doctor(), IsCancelled = false },
        new Appointment { AppointmentId = Guid.NewGuid(), Patient = new Patient(), Doctor = new Doctor(), IsCancelled = false }
    };

            var mockSet = new Mock<DbSet<Appointment>>();
            mockSet.As<IQueryable<Appointment>>().Setup(m => m.Provider).Returns(appointments.AsQueryable().Provider);
            mockSet.As<IQueryable<Appointment>>().Setup(m => m.Expression).Returns(appointments.AsQueryable().Expression);
            mockSet.As<IQueryable<Appointment>>().Setup(m => m.ElementType).Returns(appointments.AsQueryable().ElementType);
            mockSet.As<IQueryable<Appointment>>().Setup(m => m.GetEnumerator()).Returns(appointments.AsQueryable().GetEnumerator());

            _mockContext.Setup(c => c.Appointments).Returns(mockSet.Object);

            // Act
            var result = await _handler.Handle(new AppointmentsList.Query(), CancellationToken.None);

            // Assert
            Assert.IsType<Result<List<AppointmentDto>>>(result);
            // You can add more assertions here to verify the contents of the result
        }

    }
}