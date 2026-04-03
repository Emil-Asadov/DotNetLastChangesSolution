namespace DependencyInjectionType.Extensions
{
    public static class MyExtensions
    {
        public static IServiceCollection MyDependencyInjection(this IServiceCollection services)
        {
            //services.AddTransient<Test1>();
            //services.AddTransient<Test2>();

            services.AddScoped<Test1>();
            services.AddScoped<Test2>();

            //services.AddSingleton<Test1>();
            //services.AddSingleton<Test2>();

            return services;
        }
    }
}
