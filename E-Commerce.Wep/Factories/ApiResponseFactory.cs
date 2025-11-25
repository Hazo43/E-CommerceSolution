using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Wep.Factories
{
    public static class ApiResponseFactory
    {
        public static IActionResult GenerateApiValidationResponse( ActionContext actionContext)
        {
            // و ترجع الايرور بتاعتو Invalid يكون ModelState بتعمل فحص بتشوف فيه اي
            var errors = actionContext.ModelState.Where(X => X.Value.Errors.Count > 0)
                                      .ToDictionary(X => X.Key, // بتاع الايرور اللي حاصل و اللي تحت الايرور نفسو او نتيجه الايرور Key دا ال
                                      X => X.Value.Errors.Select(X => X.ErrorMessage).ToArray()); // من الايرور اللي ممكن تكون حصلت Array دي هتكون 

            var problem = new ProblemDetails
            {
                Title = " Validation Error",
                Detail = "One Or More Validation Error occurred.",
                Status = StatusCodes.Status400BadRequest,
                // Value و  Key يعني لازم Dictionary دي بتاخد
                Extensions =
                        {
                            // Key    , Value =>  اللي راجعه errors دي المفروض ال
                            { "Error" , errors }
                        }
            };
            return new BadRequestObjectResult(problem);
        }
    }
}
