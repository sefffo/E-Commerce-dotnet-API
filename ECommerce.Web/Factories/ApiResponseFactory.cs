using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Factories
{
    public static class ApiResponseFactory
    {
        public static IActionResult GenerateApiValidationResponse(ActionContext actionContext)
        {
            var Errors =
                    actionContext.ModelState.Where(e => e.Value.Errors.Count > 0)
                    .ToDictionary(x => x.Key,
                    x => x.Value.Errors.Select(x => x.ErrorMessage).ToList());
            var problem = new ProblemDetails()
            {
                Title = "Validation Errors ",
                Detail = "One Or More validation Error occurred",
                Status = StatusCodes.Status400BadRequest,
                Extensions =
                        {
                            {"Errors" , Errors }
                        }
            };

            return new BadRequestObjectResult(problem);
        }
    }
}
