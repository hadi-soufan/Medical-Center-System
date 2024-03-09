namespace Application.Core
{
    /// <summary>
    /// Represents an application exception.
    /// </summary>
    public class AppException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppException"/> class.
        /// </summary>
        /// <param name="statusCode">The HTTP status code associated with the exception.</param>
        /// <param name="message">The error message.</param>
        /// <param name="details">Optional additional details about the exception.</param>
        public AppException(int statusCode, string message, string details = null)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
    }
}
