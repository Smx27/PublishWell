namespace JPS.Middleware.ExceptionDTO
{
    /// <summary>
    /// Represents an exception that occurred in the API, providing status code, message, and details.
    /// </summary>
    public class ApiException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException"/> class with the specified status code, message, and details.
        /// </summary>
        /// <param name="statusCode">The HTTP status code associated with the exception.</param>
        /// <param name="message">A brief description of the exception.</param>
        /// <param name="details">Additional details about the exception (optional).</param>
        public ApiException(int statusCode, string message, string details = null)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }

        /// <summary>
        /// The HTTP status code associated with the exception.
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// A brief description of the exception.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Additional details about the exception (optional).
        /// </summary>
        public string Details { get; set; }
    }
}
