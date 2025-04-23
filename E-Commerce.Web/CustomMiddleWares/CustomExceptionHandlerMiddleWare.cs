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
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong");

                // Set Status Code For Response
                httpContext.Response.StatusCode = ex switch
                {

                    // Not Found
                    NotFoundException => StatusCodes.Status404NotFound,

                    // Internal Server Error
                    _ => (int)HttpStatusCode.InternalServerError
                };

                //// Set Content Type For Response
                //httpContext.Response.ContentType = "application/json";

                // Response Object
                var response = new ErrorToReturn()
                {
                    StatusCode = httpContext.Response.StatusCode,
                    ErrorMessage = ex.Message
                };

                // Return Object As Json
                await httpContext.Response.WriteAsJsonAsync(response);

            }
        }
    }
}
