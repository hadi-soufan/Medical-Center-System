using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Appoitments
{
    /// <summary>
    /// Represents a query to retrieve a list of appointments.
    /// </summary>
    public class AppointmentsList
    {
        /// <summary>
        /// Represents the query to retrieve a list of appointments.
        /// </summary>
        public class Query : IRequest<List<Appointment>>
        {

        }

        /// <summary>
        /// Represents the handler for the <see cref="Query"/> to retrieve a list of appointments.
        /// </summary>
        public class Handler : IRequestHandler<Query, List<Appointment>>
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
            /// Handles the query to retrieve a list of appointments.
            /// </summary>
            /// <param name="request">The query representing the request to retrieve a list of appointments.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A task representing the asynchronous operation. The task result is the list of appointments.</returns>
            public async Task<List<Appointment>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Appointments.ToListAsync(cancellationToken: cancellationToken);
            }
        }
    }
}
