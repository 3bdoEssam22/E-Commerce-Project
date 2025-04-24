using E_Commerce.Web.CustomMiddleWares;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Web.Factories
{
    public static class ApiResponseFactory
    {
        public static IActionResult GenerateApiValidationErrorsResponse(ActionContext Context)
        {
            var Errors = Context.ModelState
                .Where(M => M.Value.Errors.Any())
                .Select(M => new ValidationError
                {
                    Field = M.Key,
                    Errors = M.Value.Errors.Select(x => x.ErrorMessage)
                });
            var Response = new ValidationErrorToReturn
            {
                ValidationErrors = Errors
            };
            return new BadRequestObjectResult(Response);
        }
    }
}
