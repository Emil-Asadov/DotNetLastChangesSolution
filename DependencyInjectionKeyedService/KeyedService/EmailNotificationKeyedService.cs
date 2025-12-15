namespace DependencyInjectionKeyedService.KeyedService
{
    public class EmailNotificationKeyedService : INotificationKeyedService
    {
        public string Send(string message)
        {
            return $"Email: {message}";
        }
    }
}
