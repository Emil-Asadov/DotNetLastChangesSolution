
using APIMiddlewareLog.Extensions;
using APIMiddlewareLog.Middleware;
using NLog.Web;

namespace APIMiddlewareLog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region Dependency Injection
            //builder.Services.AddScoped<RequestResponseMiddleware>();
            builder.Services.DIExtensions(); //Use Extension method
            #endregion

            #region Write_Log
            builder.Logging.ClearProviders();
            //builder.Logging.SetMinimumLevel(LogLevel.Trace);
            builder.Host.UseNLog();
            #endregion


            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            #region Middleware
            //app.UseMiddleware<RequestResponseMiddleware>();
            app.APPBuilderExtensions();
            #endregion

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
