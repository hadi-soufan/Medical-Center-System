using Application.Core;
using Domain.Entities;
using MediatR;

using Persistence;

/// <summary>
/// Represents a query to retrieve details of an appointment.
/// </summary>
public class AppointmentDetails
{
    /// <summary>
    /// Represents the query to retrieve details of an appointment.
    /// </summary>
    public class Query : IRequest<Result<Appointment>>
    {
        /// <summary>
        /// Gets or sets the ID of the appointment for which details are to be retrieved.
        /// </summary>
        public Guid Id { get; set; }
    }

    /// <summary>
    /// Represents the handler for the <see cref="Query"/> to retrieve details of an appointment.
    /// </summary>
    public class Handler : IRequestHandler<Query, Result<Appointment>>
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
        public async Task<Result<Appointment>> Handle(Query request, CancellationToken cancellationToken)
        {
            var appointment = await _context.Appointments.FindAsync(request.Id);

            return Result<Appointment>.Success(appointment);
        }
    }
}
