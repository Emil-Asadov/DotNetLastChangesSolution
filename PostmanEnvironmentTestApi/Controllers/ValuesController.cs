using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace PostmanEnvironmentTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
        [HttpGet]
        [Route("Calculate")]
        public IActionResult Calculate(int a, int b)
        {
            var res = a + b;
            return Ok(res);
        }
        [HttpPost]
        [Route("Concate")]
        public IActionResult Concate(string name, string birthDate)
        {
            var res = new StringBuilder().Append(name).Append((char)32).Append(birthDate);
            return Ok(res.ToString());
        }
        [HttpGet]
        [Route("CheckCurrentDate/{currentDateStr}")]
        public IActionResult Calculate(string currentDateStr)
        {
            var res = DateTime.TryParse(currentDateStr, out var currentDate);
            if (!res)
                return NotFound();

            res = currentDate.Equals(DateTime.Now.Date);
            var jsonResult = new JsonResult(res);

            return Ok(jsonResult);
        }
    }
}
