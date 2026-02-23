using ECommerce.SharedLibirary.CommonResult;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ECommerce.Presentation.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class ApiBaseController : ControllerBase
    {

        //any common code need to be shared between all controllers can be put here

        //to handle the common response format for all endpoints in the project we can override the Ok method to wrap the data in a standard response structure



        //Handle Result Without Value 

        ///isSuccess => 400 BadRequest with the error message in the body
        ///isSuccess => 500 InternalServerError with the error message in the body
        ///isSuccess => 404 NotFound with the error message in the body
        ///isSuccess => 401 Unauthorized with the error message in the body
        ///isSuccess => 403 Forbidden with the error message in the body
        ///isSuccess => 204 NoContent with no data in the body




        protected IActionResult HandleResult(Result result)
        {
            if (result.isSuccess)
            {
                return NoContent();
            }
            else
            {
                //outside function to convert the error to the appropriate status code and message
                //gonna be used in both with value and without value result handling

                return HandleProblem(result.Errors);

            }
        }






        //the other Result with value 

        ///isSuccess => 200 OK with the data in the body
        ///isSuccess => 400 BadRequest with the error message in the body
        ///isSuccess => 500 InternalServerError with the error message in the body


        protected ActionResult HandleResult<T>(Result<T> result)
        {
            if (result.isSuccess)
            {
                return Ok(result.value);
            }
            else
            {
                //outside function to convert the error to the appropriate status code and message
                //gonna be used in both with value and without value result handling
            
                return HandleProblem(result.Errors);

            }





        }

        private ActionResult HandleProblem(IReadOnlyCollection<Error> errors)
        {
            if (errors.Count() == 0)
            {
                return Problem(statusCode: StatusCodes.Status500InternalServerError, title: "Unexpected Error Occurred ");
            }
            if (errors.All(e => e.type == ErrorType.Validation))
            {
                return HandleValidationProblem(errors);
            }
            return HandleSingleProblem(errors.First());
        }

        private ActionResult HandleSingleProblem(Error error)
        {
            return Problem(title: error.Code, detail: error.Description, type: error.type.ToString(), statusCode: GetStatusCode(error.type));

        }

        private static int GetStatusCode(ErrorType errorType)
        {
            return errorType switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
                ErrorType.Forbidden => StatusCodes.Status403Forbidden,
                ErrorType.InternalServerError => StatusCodes.Status500InternalServerError,
                ErrorType.BadRequest => StatusCodes.Status400BadRequest,
                ErrorType.InvalidCredentials => StatusCodes.Status401Unauthorized,
                _ => StatusCodes.Status500InternalServerError
            };
        }


        private ActionResult HandleValidationProblem(IReadOnlyCollection<Error> errors)
        {
            var ModelState = new ModelStateDictionary();

            foreach (var error in errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }

            return ValidationProblem(ModelState);
        }
    }
}
