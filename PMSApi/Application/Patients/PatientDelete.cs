using Application.Core;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Patients
{
    public class PatientDelete
    {
        public class Command : IRequest<Result<Unit>>
        {
            /// <summary>
            /// Gets or sets the ID of the appointment to be deleted.
            /// </summary>
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
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
            /// Handles the command to delete an appointment.
            /// </summary>
            /// <param name="request">The command representing the request to delete an appointment.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A task representing the asynchronous operation. The task result is the completion status.</returns>
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var patient = await _context.Patients.FindAsync(request.Id);

                if (patient is null) return null;

                _context.Remove(patient);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to Delete the Patient");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
