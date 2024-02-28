using Domain;
using MediatR;
using Persistence;

namespace Application.MedicalHistoreis
{
    /// <summary>
    /// Represents a query to retrieve details of a medical history.
    /// </summary>
    public class MedicalHistoryDetails
    {
        /// <summary>
        /// Represents the query to retrieve details of a medical history.
        /// </summary>
        public class Query : IRequest<MedicalHistory>
        {
            /// <summary>
            /// Gets or sets the ID of the medical history for which details are requested.
            /// </summary>
            public Guid Id { get; set; }
        }

        /// <summary>
        /// Represents the handler for the <see cref="Query"/> to retrieve details of a medical history.
        /// </summary>
        public class Handler : IRequestHandler<Query, MedicalHistory>
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
            /// Handles the query to retrieve details of a medical history.
            /// </summary>
            /// <param name="request">The query representing the ID of the medical history for which details are requested.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A task representing the asynchronous operation. The task result is the <see cref="MedicalHistory"/> object corresponding to the specified ID.</returns>
            public async Task<MedicalHistory> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.MedicalHistories.FindAsync(request.Id);
            }
        }
    }

}
