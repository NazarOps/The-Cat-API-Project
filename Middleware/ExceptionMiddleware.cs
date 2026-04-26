using System.Text.Json;
using Cat_API_Project.Exceptions;   

namespace Cat_API_Project.Middleware
{
    public class ExceptionMiddleware
    {
        // middleware exception handler for global errors, will return the error in JSON format
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }

            catch(Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred");

                await HandleExceptionAsync(context, ex);
            }


        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            int StatusCode = ex switch
            {
                UnauthorizedException => StatusCodes.Status401Unauthorized,
                NotFoundException => StatusCodes.Status404NotFound,
                BadRequestException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError,
                
            };

            context.Response.StatusCode = StatusCode;

            var response = new
            {
                message = ex switch
                {
                    UnauthorizedException => ex.Message,
                    NotFoundException => ex.Message,
                    BadRequestException => ex.Message,
                    _ => "An unexpected error occurred."
                }
            };

            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
        }
    }
}
