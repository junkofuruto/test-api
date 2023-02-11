using Microsoft.AspNetCore.Mvc;

namespace TestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private static List<string> _summaries = new()
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public WeatherForecastController(ILogger<WeatherForecastController> logger) => _logger = logger;

        [HttpGet("getbysortstrategy")]
        public IActionResult Get(int? sortStrategy)
        {
            List<string> result;

            if (sortStrategy == null) result = _summaries;
            else if (sortStrategy == 1) result = _summaries.OrderBy(x => x).ToList();
            else if (sortStrategy == -1) result = _summaries.OrderByDescending(x => x).ToList();
            else return BadRequest("Некорректное значение параметра sortStrategy");
            return Ok(result);
        }

        [HttpGet("getbyindex")]
        public IActionResult GetByIndex(int index)
        {
            return Ok(_summaries.ElementAtOrDefault(index));
        }

        [HttpGet("countof")]
        public IActionResult CountOf(string name)
        {
            return Ok(_summaries.Where(x => x.Contains(name)).Count());
        }

        [HttpPost("add")]
        public IActionResult Add(string name)
        {
            _summaries.Add(name);
            return Ok();
        }

        [HttpPut("updateat")] 
        public IActionResult Update(int index, string name)
        {
            if (index < 0 || index >= _summaries.Count)
                return BadRequest("Некорректный индекс");
            _summaries[index] = name;

            return Ok();
        }

        [HttpDelete("removeat")]
        public IActionResult Delete(int index)
        {
            if (index < 0 || index >= _summaries.Count)
                return BadRequest("Некорректный индекс");
            _summaries.RemoveAt(index);

            return Ok();
        }
    }
}