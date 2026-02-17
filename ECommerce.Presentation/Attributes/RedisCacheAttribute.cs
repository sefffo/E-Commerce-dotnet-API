using ECommerce.Services.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Text;


namespace ECommerce.Presentation.Attributes
{
    public class RedisCacheAttribute : ActionFilterAttribute
    {

        private readonly int _Time;
        public RedisCacheAttribute(int DurationInMinutes = 5)
        {
            _Time = DurationInMinutes;
        }



        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            //Get  the Cache service  ==> done 
            var CacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();


            //generate the cahceKey first 
            var CacheKey = CreateCacheKey(context.HttpContext.Request);


            //check if the chached data needed in context exsist 
            var cacheValue = await CacheService.GetAsync(CacheKey);
            // if yes return the data and then skip the end point request 

            if (cacheValue is not null)
            {

                context.Result = new ContentResult()
                {
                    Content = cacheValue,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK,
                };
                return;
            }
            // if no request the end point  and the if the response is 200 OK cache it 

            var ExcutedContext = await next.Invoke();

            if (ExcutedContext.Result is OkObjectResult result)
            {
                await CacheService.setAsync(CacheKey, result.Value, TimeSpan.FromMinutes(_Time));
            }





















        }

        private string CreateCacheKey(HttpRequest request)
        {
            StringBuilder Key = new StringBuilder();
            Key.Append(request.Path); //api/product

            foreach (var item in request.Query.OrderBy(k => k.Key))
            {
                Key.Append($"|{item.Key}-{item.Value}");
            }

            return Key.ToString(); // to get the request url to check for the data i have 
        }

    }


}
