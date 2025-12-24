using DependencyInjectionKeyedService.Enums;
using DependencyInjectionKeyedService.KeyedService;
using DependencyInjectionKeyedService.WithoutKeyedService;

namespace DependencyInjectionKeyedService
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

            #region DIContainer
            builder.Services.AddSingleton<INotificationService, EmailNotificationService>();
            builder.Services.AddSingleton<INotificationService, SmsNotificationService>();
            builder.Services.AddSingleton<INotificationService, PushNotificationService>();
            #endregion
            #region DIContainerKeyedService
            builder.Services.AddKeyedSingleton<INotificationKeyedService, EmailNotificationKeyedService>(KeyedNotificationsType.KeyedNotification.Email);
            builder.Services.AddKeyedSingleton<INotificationKeyedService, SmsNotificationKeyedService>(KeyedNotificationsType.KeyedNotification.Sms);
            builder.Services.AddKeyedSingleton<INotificationKeyedService, PushNotificationKeyedService>(KeyedNotificationsType.KeyedNotification.Push);
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
