namespace DependencyInjectionKeyedService.WithoutKeyedService
{
    public class SmsNotificationService : INotificationService
    {
        public string Send(string message)
        {
            return $"Sms: {message}";
        }
    }
}
