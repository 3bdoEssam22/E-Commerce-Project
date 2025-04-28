using DomainLayer.Exceptions;
using Shared.ErrorModels;
using System.Net;
using System.Text.Json;

namespace E_Commerce.Web.CustomMiddleWares
{
    public class CustomExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandlerMiddleWare> _logger;

        public CustomExceptionHandlerMiddleWare(RequestDelegate Next
            , ILogger<CustomExceptionHandlerMiddleWare> Logger)
        {
            _next = Next;
            _logger = Logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);

                await HandleNotFoundEndPointAsync(httpContext);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong");

                await HandleExceptionAsync(httpContext, ex);

            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            // Response Object
            var response = new ErrorToReturn()
            {
                StatusCode = httpContext.Response.StatusCode,
                ErrorMessage = ex.Message
            };

            // Set Status Code For Response
            response.StatusCode = ex switch
            {

                // Not Found
                NotFoundException => StatusCodes.Status404NotFound,

                // Unauthorized
                UnauthorizedException => StatusCodes.Status401Unauthorized,

                // Bad Request
                BadRequestException badRequestException => GetBadRequestErrors(badRequestException, response),

                // Internal Server Error
                _ => StatusCodes.Status500InternalServerError
            };



            // Return Object As Json
            await httpContext.Response.WriteAsJsonAsync(response);
        }

        private static int GetBadRequestErrors(BadRequestException badRequestException, ErrorToReturn response)
        {
            response.Errors = badRequestException.Errors;
            return StatusCodes.Status400BadRequest
        }

        private static async Task HandleNotFoundEndPointAsync(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var Response = new ErrorToReturn
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = $"Endpoint {httpContext.Request.Path} Is Not Found"
                };
                await httpContext.Response.WriteAsJsonAsync(Response);
            }
        }
    }
}
