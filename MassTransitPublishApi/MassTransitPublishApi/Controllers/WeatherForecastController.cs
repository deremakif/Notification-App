using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Test.Messages;

namespace MassTransitPublishApi.Controllers
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
        private readonly IBusControl _bus;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IBusControl bus)
        {
            _logger = logger;
            _bus = bus;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            _bus.Publish(new TestMessage { Body = "body 1", Title = "title 1", To = "fNSbPuo4IE8:APA91bFqP2mmKEEsqI8pnzrqpiWtJmiT-YviswUAFkgaCh_Z6ZjwaaScJjIEM7de06X0T2DwAhynDZQBkWSGAlUR-SHoThAOAvs2UqTL5UcXtl8R53L7W-UwjJyb1KW0eYHk5UPrlag0" });

            var rng = new Random();
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
