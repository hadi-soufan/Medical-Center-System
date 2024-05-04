using Application.Core;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Photos
{
    public class Add
    {
        /// <summary>
        /// Represents the command to add a new photo.
        /// </summary>
        public class Command : IRequest<Result<PatientPhoto>>
        {
            /// <summary>
            /// Gets or sets the photo file to be added.
            /// </summary>
            public IFormFile File { get; set; }
            public string Description { get; set; }
            public string UserId { get; set; }
        }

        /// <summary>
        /// Handler for the <see cref="Command"/> to add a new photo.
        /// </summary>
        public class Handler : IRequestHandler<Command, Result<PatientPhoto>>
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
            /// Handles the <see cref="Command"/> to add a new photo.
            /// </summary>
            /// <param name="request">The command to add a new photo.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A result indicating success or failure along with the added photo.</returns>
            public async Task<Result<PatientPhoto>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _context.Users
                    .Include(p => p.PatientPhotos)
                    .FirstOrDefaultAsync(x => x.Id == request.UserId);

                if (user is null) return null;

                var photoUploadResult = await _photoAccessor.AddPhoto(request.File);

                var photo = new PatientPhoto
                {
                    Url = photoUploadResult.Url,
                    Id = photoUploadResult.PublicId,
                    AppUserId = user.Id

                };

                user.PatientPhotos.Add(photo);

                var result = await _context.SaveChangesAsync() > 0;

                if (result) return Result<PatientPhoto>.Success(photo);

                return Result<PatientPhoto>.Failure("Problem adding photo");
            }


        }
    }
}
