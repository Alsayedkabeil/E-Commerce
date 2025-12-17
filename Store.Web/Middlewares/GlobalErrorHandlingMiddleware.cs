using Store.Domain.Exceptions;
using Store.Shared.ErrorModels;

namespace Store.Web.Middlewares
{
    // Middleware class for global error handling
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next; // Field to hold the next middleware delegate
        private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;

        public GlobalErrorHandlingMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlingMiddleware> logger) // Constructor to initialize the middleware with the next delegate
        {
            _next = next; // Assign the next delegate to a private field + Next middleware in the pipeline
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context); // Call the next middleware in the pipeline
                // If no exception occurs, there is no Endpoint or The Route is not matched:
                if (context.Response.StatusCode == StatusCodes.Status404NotFound) // 404 Not Found
                {
                    context.Response.ContentType = "application/json"; // Set content type to JSON
                    var responseBody = new ErrorDetails()
                    {
                        StatusCode = StatusCodes.Status404NotFound, // 404 Not Found
                        ErrorMessage = $"The End Point '{context.Request.Path}' was not found..!!" // Custom message for 404 Not Found
                    };
                    await context.Response.WriteAsJsonAsync(responseBody); // Write the response body as JSON
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, ex.Message); // Log the exception details (Save Erros in a file or database)

                // 1. Set the response status code and content type
                // context.Response.StatusCode = StatusCodes.Status500InternalServerError;  // Internal Server Error
                context.Response.ContentType = "application/json"; // Set content type to JSON
                // 2. Set the response body
                var responseBody = new ErrorDetails()
                {
                    //StatusCode = StatusCodes.Status500InternalServerError,
                    ErrorMessage = ex.Message
                };
                // 3. Return My Response
                responseBody.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    BadException => StatusCodes.Status400BadRequest,
                    ValidationException => HandleValidationExceptionAsync((ValidationException)ex , responseBody),
                    UnAuthorizedException => StatusCodes.Status401Unauthorized,
                    _ => StatusCodes.Status500InternalServerError
                };
                context.Response.StatusCode = responseBody.StatusCode;
                await context.Response.WriteAsJsonAsync(responseBody);
            }
        }

        #region Private Method To ValidationException[Errors]
        private static int HandleValidationExceptionAsync(ValidationException ex,ErrorDetails response)
        {
            response.Errors = ex.Errors; // Set the validation errors in the response
            return StatusCodes.Status400BadRequest; // Return 400 Bad Request status code
        }
        #endregion
    }
}
