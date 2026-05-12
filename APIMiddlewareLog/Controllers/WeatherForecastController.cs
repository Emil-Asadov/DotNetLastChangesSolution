using APIMiddlewareLog.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml;

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
        public IActionResult CreateEmployee([FromBody] Employee employee)
        {
            var response = new JsonResult(new { message = "Employee created" })
            {
                ContentType = "application/json",
                SerializerSettings = new JsonSerializerSettings
                {
                    Formatting = Newtonsoft.Json.Formatting.Indented // pretty JSON
                },
                StatusCode = 200
            };

            var jsonResponse = new
            {
                message = "Employee created"
            };

            return Ok(jsonResponse);
        }
    }
}
