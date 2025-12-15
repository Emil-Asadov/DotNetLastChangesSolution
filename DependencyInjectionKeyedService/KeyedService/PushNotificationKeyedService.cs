namespace DependencyInjectionKeyedService.KeyedService
{
    public class PushNotificationKeyedService : INotificationKeyedService
    {
        public string Send(string message)
        {
            return $"Push: {message}";
        }
    }
}
