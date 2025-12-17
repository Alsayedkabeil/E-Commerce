using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Store.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Presentation.Attributes
{
    public class CacheAttribute(int duration) : Attribute, IAsyncActionFilter
    {
        #region To Caching Request
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IServiceManager>().CachingServices;
            var cacheKey = GenerateCacheKey(context.HttpContext.Request);
            var result = await cacheService.GetChacheValueAsync(cacheKey);

            if (!string.IsNullOrEmpty(result)) // Return cached response if available
            {
                // Return the cached response
                context.Result = new ContentResult() // Create a ContentResult to return the cached content
                {
                    Content = result,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
                return; // Short-circuit the action execution
            }
            else
            {
                // Execute the Endpoint 
                var contextResult = await next.Invoke(); // Proceed to the action execution
                if (contextResult.Result is OkObjectResult ok)
                {
                    await cacheService.SetChacheValueAsync(cacheKey, ok.Value, TimeSpan.FromSeconds(duration));
                }
            }
        }
        #endregion

        #region GenerateCacheKey
        private string GenerateCacheKey(HttpRequest request) // Generate a unique cache key based on the request path and query parameters
        {
            var keyBuilder = new StringBuilder(); // Use StringBuilder for efficient string concatenation
            keyBuilder.Append($"{request.Path}"); // Start with the request path
            foreach (var (key, value) in request.Query.OrderBy(x => x.Key)) // Append sorted query parameters to ensure consistent key generation
            {
                keyBuilder.Append($"|{key}-{value}"); // Use '|' and '-' as delimiters for clarity
            }
            return keyBuilder.ToString(); // Return the generated cache key
        } 
        #endregion
    }
}
