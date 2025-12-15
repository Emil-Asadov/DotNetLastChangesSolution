namespace DependencyInjectionKeyedService.WithoutKeyedService
{
    public class PushNotificationService : INotificationService
    {
        public string Send(string message)
        {
            return $"Push: {message}";
        }
    }
}
