using E_Commerce.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Wep.CustomMiddleWares
{
    public class ExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleWare> _logger;

        public ExceptionHandlerMiddleWare(RequestDelegate Next , ILogger<ExceptionHandlerMiddleWare> logger)
        {
            _next = Next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                // دا لو  كل حاجه صح هيبدا ان هز يعمل استدعاء لكل اللي بعتها و تمشي واجده واحده 
               await _next.Invoke(httpContext);
               // EndPoint دا هيتنفذ في حاله انو معرفش يوصل ل
                if( httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    var Problem = new ProblemDetails()
                    {
                        Title = "Error Will Processing The HTTP Request - EndPoint Not Found",
                        Status = StatusCodes.Status404NotFound,
                        Detail = $" EndPoint {httpContext.Request.Path} Not Found",
                        Instance = httpContext.Request.Path
                        
                    };
                    await httpContext.Response.WriteAsJsonAsync(Problem);
                }
            }
            catch (Exception ex)
            {
                // بقا هنبدا نعمل الخطوات دي Exception في حاله ان فيه

                // Logging 

                _logger.LogError(ex, "Something went wrong");

                // Return Custom Error Response 



                var Problem = new ProblemDetails()
                {
                    Title = "An unexpected error occurred!",
                    Detail = ex.Message,
                    Instance = httpContext.Request.Path,
                    Status = ex switch
                    {
                        NotFoundException => StatusCodes.Status404NotFound,
                        _ => StatusCodes.Status500InternalServerError,
                    }
                };
                httpContext.Response.StatusCode = Problem.Status.Value;
                await httpContext.Response.WriteAsJsonAsync(Problem);
            }
        }
    }
}
