using Application.Photos;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces
{
    /// <summary>
    /// Interface for interacting with photo storage.
    /// </summary>
    public interface IPhotoAccessor
    {
        /// <summary>
        /// Adds a new photo to the storage.
        /// </summary>
        /// <param name="file">The photo file to be added.</param>
        /// <returns>A task representing the asynchronous operation, containing the result of the photo upload.</returns>
        Task<PhotoUploadResult> AddPhoto(IFormFile file);

        /// <summary>
        /// Deletes a photo from the storage by its public ID.
        /// </summary>
        /// <param name="publicId">The public ID of the photo to be deleted.</param>
        /// <returns>A task representing the asynchronous operation, containing the URL of the deleted photo.</returns>
        Task<string> DeletePhoto(string publicId);
    }
}
