using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using Store.G04.Core.Sevices.Contract;
using System.Text;

namespace Store.G04.APIs.Attributes
{
    public class CachedAttribute :Attribute, IAsyncActionFilter
    {
        private readonly int _expireTime;

        public CachedAttribute(int expireTime)
        {
            _expireTime = expireTime;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
           var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();

            var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);

            var cacheResponse = await cacheService.GetCacheKeyAsync(cacheKey);

            if(!string.IsNullOrEmpty(cacheResponse))
            {
                var contentResult = new ContentResult()
                {
                    Content = cacheResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };

                context.Result = contentResult;
                return;
            }

            var executedContext =  await next();
            if(executedContext.Result is OkObjectResult response)
            {
               await cacheService.SetCatcheKeyAsync(cacheKey, response.Value, TimeSpan.FromSeconds(_expireTime));
            }
           
        } 
        private string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            var CacheKey = new StringBuilder();
            CacheKey.Append(handler: $"{request.Path}");

            foreach (var (key, value) in request.Query.OrderBy(X => X.Key))
            {
                CacheKey.Append(handler: $"|{key}-{value}");
            }
            return CacheKey.ToString();
        }
    }
}
