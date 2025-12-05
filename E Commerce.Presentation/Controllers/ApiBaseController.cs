using E_Commerce.Shared.CommonResult;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiBaseController : ControllerBase
    {
        // Handel Resulut Without Value (Void)
        // 1 -> If Result Is Success Return NoContent (204) 
        // 2 -> If Result Is Failure Return Problem With Status Code And Error Details 

        protected IActionResult HandleResult(Result result)
        {
            if(result.IsSuccess) // true
                return NoContent();
            else
                return HandleProblem(result.Errors);
        }


        // Handel Resulut With bValue 
        // 1 -> If Result Is Success Return OK 200 With Value 
        // 2 -> If Result Is Failure Return Problem With Status Code And Error Details 

        protected ActionResult<TValue> HandleResult<TValue>(Result<TValue> result)
        {
            if (result.IsSuccess) // true 
                return Ok(result.Value);
            else
                return HandleProblem(result.Errors);
        }


        private ActionResult HandleProblem(IReadOnlyList<Error> errors)
        {
            // If Not Error Are Provided , Return 500 Error
            if (errors.Count == 0)
                return Problem(statusCode: StatusCodes.Status500InternalServerError, title: "An Unexpected Error Occurred.");

            // If All Errors are Validation Error , Handle Them As Validation Problem 
            if (errors.All(e => e.Type == ErrorType.Validation))
                return HandleValidationProblem(errors);
            // If There is Only One Error , Handle It As A Single Error Problem
            return HandleSingleErrorProblem(errors[0]);

        }


        // If There is Only One Error , Handle It As A Single Error Problem
        private ActionResult HandleSingleErrorProblem(Error error)
        {
            return Problem(
                title: error.Code,
                detail: error.Description,
                type: error.Type.ToString(),
                statusCode: MapErrorTypeToStatusCode(error.Type));
          
        }
        // Map Error Type 
        private static int MapErrorTypeToStatusCode(ErrorType errorType)
        {
            return errorType switch
            {
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
                ErrorType.Forbidden => StatusCodes.Status403Forbidden,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.InvalidCrendentials => StatusCodes.Status401Unauthorized,
                ErrorType.Failure => StatusCodes.Status500InternalServerError,
                _ => StatusCodes.Status500InternalServerError,

            };
        }

        // If All Errors are Validation Error , Handle Them As Validation Problem 
        private ActionResult HandleValidationProblem(IReadOnlyList<Error> errors)
        {
            var modelState = new ModelStateDictionary();
            foreach (var error in errors)  // Key    ,    Value 
                modelState.AddModelError( error.Code , error.Description );
            return ValidationProblem(modelState);
        }

    }
}
