namespace DependencyInjectionKeyedService.WithoutKeyedService
{
    public class EmailNotificationService : INotificationService
    {
        public string Send(string message)
        {
            return $"Email: {message}";
        }
    }
}
