using System.ComponentModel;

namespace DependencyInjectionKeyedService.Enums
{
    public class KeyedNotificationsType
    {
        public enum KeyedNotification
        {
            [Description("Sms type")] Sms = 1,
            [Description("Email type")] Email = 2,
            [Description("Push type")] Push = 3
        }
    }
}
