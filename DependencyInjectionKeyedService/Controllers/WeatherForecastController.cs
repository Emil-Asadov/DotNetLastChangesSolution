using DependencyInjectionKeyedService.WithoutKeyedService;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjectionKeyedService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        #region DI
        private readonly INotificationService _emailNotificationService;
        private readonly INotificationService _smsNotificationService;
        private readonly INotificationService _pushNotificationService;
        #endregion 

        public WeatherForecastController(ILogger<WeatherForecastController> logger, INotificationService emailNotificationService, INotificationService smsNotificationService, INotificationService pushNotificationService)
        {
            _logger = logger;
            _emailNotificationService = emailNotificationService;
            _smsNotificationService = smsNotificationService;
            _pushNotificationService = pushNotificationService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpGet]
        [Route("GetSmsNotification")]
        public IActionResult GetSmsNotification()
        {
            var res = _smsNotificationService.Send("notification");

            return Ok(res);
        }
        [HttpGet]
        [Route("GetEmailNotification")]
        public IActionResult GetEmailNotification()
        {
            var res = _emailNotificationService.Send("notification");

            return Ok(res);
        }
        [HttpGet]
        [Route("GetPushNotification")]
        public IActionResult GetPushNotification()
        {
            var res = _pushNotificationService.Send("notification");

            return Ok(res);
        }
    }
}
