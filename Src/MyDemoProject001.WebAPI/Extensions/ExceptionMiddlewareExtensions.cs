using Microsoft.AspNetCore.Builder;
using MyDemoProject001.WebAPI.Common;

namespace MyDemoProject001.WebAPI.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}
