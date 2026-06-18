using System.Net;
using System.Text.Json;

namespace NotesAPI.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(
            RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (KeyNotFoundException ex)
            {
                await HandleExceptionAsync(
                    context,
                    HttpStatusCode.NotFound,
                    ex.Message);
            }
            catch (ArgumentException ex)
            {
                await HandleExceptionAsync(
                    context,
                    HttpStatusCode.BadRequest,
                    ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                await HandleExceptionAsync(
                    context,
                    HttpStatusCode.Unauthorized,
                    ex.Message);
            }
            catch (Exception)
            {
                await HandleExceptionAsync(
                    context,
                    HttpStatusCode.InternalServerError,
                    "An unexpected error occurred.");
            }
        }

        private static async Task HandleExceptionAsync(
            HttpContext context,
            HttpStatusCode statusCode,
            string message)
        {
            context.Response.ContentType =
                "application/json";

            context.Response.StatusCode =
                (int)statusCode;

            var response = new
            {
                StatusCode = (int)statusCode,
                Message = message
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
    }
}