using Application.Core;
using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Appointments
{
    /// <summary>
    /// Provides functionality to delete an appointment.
    /// </summary>
    public class AppointmentDelete
    {

        /// <summary>
        /// Command to delete an appointment.
        /// </summary>
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
        }

        /// <summary>
        /// Handler to process the AppointmentDelete command.
        /// </summary>
        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly ApplicationDbContext _context;
            private readonly IAppointmentUpdateSender _appointmentDeleteSender;

            public Handler(ApplicationDbContext context, IAppointmentUpdateSender appointmentDeleteSender)
            {
                _context = context;
                _appointmentDeleteSender = appointmentDeleteSender;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var appointment = await _context.Appointments.FindAsync(new object[] { request.Id }, cancellationToken: cancellationToken);

                    if (appointment is null) return Result<Unit>.Failure("Appointment not found");

                    appointment.IsCancelled = true;

                    var result = await _context.SaveChangesAsync(cancellationToken) > 0;

                    if (!result) return Result<Unit>.Failure("Failed to Delete the Appointment");

                    await _appointmentDeleteSender.NotifyAppointmentDeletion("AppointmentDeleted", request.Id);

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