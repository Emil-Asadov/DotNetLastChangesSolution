using APIMiddlewareLog.Middleware;
using System.Runtime.CompilerServices;

namespace APIMiddlewareLog.Extensions
{
    public static class MyExtensions
    {
        public static IServiceCollection DIExtensions(this IServiceCollection serviceCollections)
        {
            serviceCollections.AddScoped<RequestResponseMiddleware>();

            return serviceCollections;
        }

        public static IApplicationBuilder APPBuilderExtensions(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<RequestResponseMiddleware>();

            return applicationBuilder;
        }
    }
}
