using Application.Appoitments;
using Application.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Appointments
{
    /// <summary>
    /// Handles listing appointments.
    /// </summary>
    public class AppointmentsList
        {
        /// <summary>
        /// Query to retrieve a list of appointments.
        /// </summary>
        public class Query : IRequest<Result<List<AppointmentDto>>>
            {

            }

        /// <summary>
        /// Handler for the appointment list query.
        /// </summary>
        public class Handler : IRequestHandler<Query, Result<List<AppointmentDto>>>
        {
            private readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            /// <summary>
            /// Handles the appointment list query.
            /// </summary>
            /// <param name="request">The query request.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A task representing the asynchronous operation that returns the result of the query.</returns>
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
