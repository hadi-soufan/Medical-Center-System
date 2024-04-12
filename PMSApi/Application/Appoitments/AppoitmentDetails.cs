using Application.Appoitments;
using Application.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Appointments
{
    /// <summary>
    /// Provides functionality to retrieve details of an appointment.
    /// </summary>
    public class AppointmentDetails
    {
        /// <summary>
        /// Query to retrieve details of an appointment.
        /// </summary>
        public class Query : IRequest<Result<AppointmentDto>>
        {
            public Guid Id { get; set; }
        }

        /// <summary>
        /// Handler to process the AppointmentDetails query.
        /// </summary>
        public class Handler(ApplicationDbContext context) : IRequestHandler<Query, Result<AppointmentDto>>
        {
            /// <summary>
            /// Handles the AppointmentDetails query.
            /// </summary>
            /// <param name="request">The details query.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A result containing details of the appointment.</returns>
            public async Task<Result<AppointmentDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var appointment = await context.Appointments
                    .Include(a => a.Patient)
                    .Select(a => new AppointmentDto
                    {
                        AppointmentId = a.AppointmentId,
                        AppointmentDateStart = a.AppointmentDateStart,
                        AppointmentDateEnd = a.AppointmentDateEnd,
                        AppointmentStatus = a.AppointmentStatus,
                        AppointmentType = a.AppointmentType,
                        Notes = a.Notes,
                        PatientUsername = a.Patient.User.UserName,
                        DoctorUsername = a.Doctor.User.UserName
                    })
                    .FirstOrDefaultAsync(a => a.AppointmentId == request.Id, cancellationToken);

                    if (appointment is null) return Result<AppointmentDto>.Failure("Appointment not found");
                    return Result<AppointmentDto>.Success(appointment);
                }
                catch (Exception ex)
                {
                    return Result<AppointmentDto>.Failure(ex.Message);
                }
            }
        }
    }
}