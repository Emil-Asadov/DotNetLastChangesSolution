using Microsoft.AspNetCore.Mvc;

namespace CancellationTokenUse.Controllers
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
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
        [Route("WithoutCancellationToken")]
        public async Task<IActionResult> WithoutCancellationToken()
        {
            await Task.Delay(10000);

            Console.WriteLine("Process successfully complited!");
            return NoContent();
        }

        [HttpGet]
        [Route("WithCancellationToken")]
        public async Task<IActionResult> WithCancellationToken(CancellationToken cancellationToken)
        {
            try
            {
                await Task.Delay(10000, cancellationToken);

                Console.WriteLine("Process successfully complited!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Process successfully complited!");
                throw new Exception(ex.Message);
            }

            return NoContent();
        }
    }
}
