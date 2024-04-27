using System.Net;
using System.Text.Json;
using JPS.Middleware.ExceptionDTO;
using PublishWell.API.Interfaces;

namespace API.Middleware
{
    /// <summary>
    /// Middleware component that intercepts exceptions, logs them, and returns a JSON error response.
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Logger instance for logging exceptions.
        /// </summary>
        public ILogger<ExceptionMiddleware> _logger { get; }

        /// <summary>
        /// The current hosting environment (Development, Staging, Production, etc.).
        /// </summary>
        public IHostEnvironment _env { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next delegate in the middleware pipeline.</param>
        /// <param name="logger">The logger instance for logging exceptions.</param>
        /// <param name="env">The current hosting environment.</param>
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger,
                                    IHostEnvironment env)
        {
            _env = env;
            _logger = logger;
            _next = next;
        }

        /// <summary>
        /// Invokes the middleware asynchronously. Catches exceptions, logs them, and returns a JSON error response.
        /// </summary>
        /// <param name="context">The current HTTP context.</param>
        /// <param name="log">The exception log repository.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task InvokeAsync(HttpContext context, IExceptionLogRepository log)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                log.LogException(ex);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = _env.IsDevelopment() ?
                    new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString()) :
                    new ApiException(context.Response.StatusCode, ex.Message, "Internal Server Error");

                var option = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(response, option);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
