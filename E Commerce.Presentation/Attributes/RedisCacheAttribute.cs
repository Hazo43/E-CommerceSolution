using E_Commerce.Services_Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Attributes
{
    internal class RedisCacheAttribute : ActionFilterAttribute
    {
        private readonly int durationInMin;

        public RedisCacheAttribute( int DurationInMin = 5)
        {
            durationInMin = DurationInMin;
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Get Cach Service From DI Container
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
           
            // Create Cache Key Based On path And Query string 
            var cacheKey = CreateCacheKey(context.HttpContext.Request);

            // Check Of Cached Data Exists  
            var cacheValue = await cacheService.GetAsync(cacheKey);
            
            // If Exists , Return Cached Data Skip Executing Of EndPoint (Url)
            if (cacheValue is not null) // cache موجوده في ال Data دي في حاله ان ال
            {
                context.Result = new ContentResult()
                {
                    Content = cacheValue,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK,
                };
                return;
            }

            // If Not Exists , Execute The EndPoint and Store The Result In Cache IF Result => 200 OK 
            var ExecutedContext = await next.Invoke();
            if (ExecutedContext.Result is OkObjectResult result)
            {
                await cacheService.SetAsync(cacheKey, result.Value!, TimeSpan.FromMinutes(durationInMin));
            }


        }

        // api/products 
        // api/products?/brandId=2&typeId=1
        // api/products?/&typeId=1
        // api/products?/brandId=2&typeId=1
        private string CreateCacheKey(HttpRequest request)
        {
            StringBuilder Key = new StringBuilder();
            Key.Append(request.Path); // api/products
            foreach (var item in request.Query.OrderBy( X => X.Key))
            {
                Key.Append($"|{item.Key}-{item.Value}"); // api/products|brandId-2|typeId-1
            }
            return Key.ToString();
        }
    }
}
