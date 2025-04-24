using System.Net;

namespace E_Commerce.Web.CustomMiddleWares
{
    public class ValidationErrorToReturn
    {
        public int StatusCode { get; set; } = (int)HttpStatusCode.BadRequest;
        public string Message { get; set; } = "Validation Error";
        public IEnumerable<ValidationError> ValidationErrors { get; set; } = [];
    }
}
