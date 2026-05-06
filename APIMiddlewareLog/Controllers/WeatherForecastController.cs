using APIMiddlewareLog.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIMiddlewareLog.Controllers
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
        [Route("Get-Employee")]
        public IActionResult GetEmployee()
        {
            var cls = new Employee(1, "Hhhhdhd HHHhasdsd");

            return Ok(cls);
        }

        [HttpPost("Create-Employee")]
        //[Route("Create-Employee")]
        public IActionResult CreateEmployee([FromBody] EmployeeJson employee)
        {
            return Ok("Employee created");
        }
    }
}
