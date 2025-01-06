using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using SiAB.API.Constants;

namespace SiAB.API.Handlers
{
    public sealed class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        private readonly Dictionary<Type, Func<Exception, string>> _exceptionMessages;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
            _exceptionMessages = new Dictionary<Type, Func<Exception, string>>
                {
                    { typeof(ArgumentNullException), ex => $"A required argument was null: {ex.Message}" },
                    { typeof(ArgumentException), ex => $"An argument provided was invalid: {ex.Message}" },
                    { typeof(InvalidOperationException), ex => $"The operation is not valid in the current state: {ex.Message}" },
                    { typeof(KeyNotFoundException), ex => $"{ExceptionConstant.KeyNotFoundException}: {ex.Message}" },
                    { typeof(SqlException), ex => $"A database error occurred: {ex.Message}" }
                    // Add more custom messages for other exception types as needed
                };
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var request = httpContext.Request;

            string requestnfo = string.Concat("Method: ", request.Method,
                "\nHost: ", request.Host, "\nEndPoint: ", request.Path,
                "\nQueryString: ", request.QueryString);

            _logger.LogError(exception, "Exception occurred: {Message}", requestnfo);

            var problemDetails = new ProblemDetails
            {
                Title = "Error",
                Status = StatusCodes.Status500InternalServerError,
                Detail = GetCustomMessage(exception, requestnfo)
            };

            httpContext.Response.StatusCode = problemDetails.Status.Value;
            httpContext.Response.ContentType = "application/json; charset=utf-8";

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);

            return true;
        }

        private string GetCustomMessage(Exception exception, string info)
        {
            if (_exceptionMessages.TryGetValue(exception.GetType(), out var customMessageFunc))
            {
                return customMessageFunc(exception);
            }

            // Default message if no custom message is found
            return exception.Message;
        }
    }
}
