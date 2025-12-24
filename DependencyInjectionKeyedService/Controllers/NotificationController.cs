using DependencyInjectionKeyedService.Enums;
using DependencyInjectionKeyedService.KeyedService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjectionKeyedService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        #region DIKeyed
        private readonly INotificationKeyedService _emailNotificationKeyedService;
        private readonly INotificationKeyedService _smsNotificationKeyedService;
        private readonly INotificationKeyedService _pushNotificationKeyedService;
        #endregion
        public NotificationController(IServiceProvider serviceProvider)
        {
            _emailNotificationKeyedService = serviceProvider.GetRequiredKeyedService<INotificationKeyedService>(KeyedNotificationsType.KeyedNotification.Email);
            _smsNotificationKeyedService = serviceProvider.GetRequiredKeyedService<INotificationKeyedService>(KeyedNotificationsType.KeyedNotification.Sms);
            _pushNotificationKeyedService = serviceProvider.GetRequiredKeyedService<INotificationKeyedService>(KeyedNotificationsType.KeyedNotification.Push);
        }

        [HttpGet]
        [Route("GetSmsKeyedNotification")]
        public IActionResult GetSmsKeyedNotification()
        {
            var res = _smsNotificationKeyedService.Send("notification");

            return Ok(res);
        }
        [HttpGet]
        [Route("GetEmailKeyedNotification")]
        public IActionResult GetEmailKeyedNotification()
        {
            var res = _emailNotificationKeyedService.Send("notification");

            return Ok(res);
        }
        [HttpGet]
        [Route("GetPushKeyedNotification")]
        public IActionResult GetPushKeyedNotification()
        {
            var res = _pushNotificationKeyedService.Send("notification");

            return Ok(res);
        }
    }
}
