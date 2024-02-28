using AutoMapper;
using Domain;
using MediatR;

using Persistence;

/// <summary>
/// Represents a command to update a medical history.
/// </summary>
public class MedicalHistoryUpdate
{
    /// <summary>
    /// Represents the command to update a medical history.
    /// </summary>
    public class Command : IRequest
    {
        /// <summary>
        /// Gets or sets the medical history to be updated.
        /// </summary>
        public MedicalHistory MedicalHistory { get; set; }
    }

    /// <summary>
    /// Represents the handler for the <see cref="Command"/> to update a medical history.
    /// </summary>
    public class Handler : IRequestHandler<Command>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="Handler"/> class with the specified application database context and AutoMapper mapper.
        /// </summary>
        /// <param name="context">The application database context.</param>
        /// <param name="mapper">The AutoMapper mapper.</param>
        public Handler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the command to update a medical history.
        /// </summary>
        /// <param name="request">The command representing the request to update the medical history.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing the asynchronous operation. The task result is the completion status.</returns>
        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            request.MedicalHistory.CreatedAt = DateTime.UtcNow;
            request.MedicalHistory.UpdatedAt = DateTime.UtcNow;

            var medicalHistory = await _context.MedicalHistories.FindAsync(request.MedicalHistory.MedicalHistoryId);

            _mapper.Map(request.MedicalHistory, medicalHistory);

            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
