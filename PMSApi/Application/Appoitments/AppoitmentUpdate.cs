using Application.Appoitments;
using Application.Core;
using Domain.Entities;
using MediatR;
using Persistence;

namespace Application.Appointments
{
    public class AppointmentUpdate
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
            public Appointment Appointment { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var appointment = await _context.Appointments.FindAsync(request.Id);

                if (appointment == null)
                {
                    return Result<Unit>.Failure("Appointment not found");
                }

                // Update the appointment properties
                appointment.AppointmentDate = request.Appointment.AppointmentDate;
                appointment.AppointmentStatus = request.Appointment.AppointmentStatus;
                appointment.AppointmentType = request.Appointment.AppointmentType;
                appointment.Notes = request.Appointment.Notes;

                // Save changes to the database
                var result = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (!result)
                {
                    return Result<Unit>.Failure("Failed to update appointment");
                }

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
