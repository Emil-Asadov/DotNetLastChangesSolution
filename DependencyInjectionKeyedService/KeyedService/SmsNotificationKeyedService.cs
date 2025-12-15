namespace DependencyInjectionKeyedService.KeyedService
{
    public class SmsNotificationKeyedService : INotificationKeyedService
    {
        public string Send(string message)
        {
            return $"Sms: {message}";
        }
    }
}
