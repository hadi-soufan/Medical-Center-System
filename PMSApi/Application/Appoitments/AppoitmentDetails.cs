using Application.Appoitments;
using Application.Core;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Appointments
{
    /// <summary>
    /// Represents a query to retrieve details of an appointment.
    /// </summary>
    public class AppointmentDetails
    {
        /// <summary>
        /// Represents the query to retrieve details of an appointment.
        /// </summary>
        public class Query : IRequest<Result<AppointmentDto>>
        {
            /// <summary>
            /// Gets or sets the ID of the appointment for which details are to be retrieved.
            /// </summary>
            public Guid Id { get; set; }
        }

        /// <summary>
        /// Represents the handler for the <see cref="Query"/> to retrieve details of an appointment.
        /// </summary>
        public class Handler : IRequestHandler<Query, Result<AppointmentDto>>
        {
            private readonly ApplicationDbContext _context;

            /// <summary>
            /// Initializes a new instance of the <see cref="Handler"/> class with the specified application database context.
            /// </summary>
            /// <param name="context">The application database context.</param>
            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            /// <summary>
            /// Handles the query to retrieve details of an appointment.
            /// </summary>
            /// <param name="request">The query representing the request to retrieve details of an appointment.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A task representing the asynchronous operation. The task result is the appointment details.</returns>
            public async Task<Result<AppointmentDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var appointment = await _context.Appointments
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
                    .FirstOrDefaultAsync(a => a.AppointmentId == request.Id, cancellationToken);

                if (appointment == null)
                {
                    return Result<AppointmentDto>.Failure("Appointment not found");
                }

                

                return Result<AppointmentDto>.Success(appointment);
            }
        }
    }
}