using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Datadog.Trace;


namespace weatherapi.Controllers
{
   
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
    
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chillys", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            
            var scope = Tracer.Instance.ActiveScope;
            var rng = new Random();
            using (var parentScope =
       Tracer.Instance.StartActive("weatherforecast.get"))
{
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
}
        }
    }
}


