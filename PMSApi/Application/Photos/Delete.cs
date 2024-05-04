using Application.Core;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Persistence;

namespace Application.Photos
{
    public class Delete
    {
        /// <summary>
        /// Represents the command to delete a photo.
        /// </summary>
        public class Command : IRequest<Result<Unit>>
        {
            /// <summary>
            /// Gets or sets the ID of the photo to be deleted.
            /// </summary>
            public string Id { get; set; }
        }

        /// <summary>
        /// Handler for the <see cref="Command"/> to delete a photo.
        /// </summary>
        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly ApplicationDbContext _context;
            private readonly IPhotoAccessor _photoAccessor;
            private readonly IUserAccessor _userAccessor;

            /// <summary>
            /// Initializes a new instance of the <see cref="Handler"/> class.
            /// </summary>
            /// <param name="context">The data context.</param>
            /// <param name="photoAccessor">The photo accessor service.</param>
            /// <param name="userAccessor">The user accessor service.</param>
            public Handler(ApplicationDbContext context, IPhotoAccessor photoAccessor, IUserAccessor userAccessor)
            {
                _context = context;
                _photoAccessor = photoAccessor;
                _userAccessor = userAccessor;
            }

            /// <summary>
            /// Handles the <see cref="Command"/> to delete a photo.
            /// </summary>
            /// <param name="request">The command to delete a photo.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A result indicating success or failure.</returns>
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
              
                var photo = await _context.PatientPhotos.FindAsync(request.Id);

                if (photo is null)
                    return Result<Unit>.Failure("photo not found");

                _context.Remove(photo);

                var result = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (!result)
                    return Result<Unit>.Failure("Failed to delete photo");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
