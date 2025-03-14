using EHost.Contract.Service;
using EWebServer.Db;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EWebServer.Controllers
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
        private readonly ITenant tenant;
        private readonly IServiceProvider serviceProvider;
        private readonly EnvironmentDbContext environmentDbContext;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,IServiceProvider serviceProvider,ITenant tenant, EnvironmentDbContext environmentDbContext)
        {
            _logger = logger;
            this.tenant = tenant;
            this.serviceProvider = serviceProvider;
            this.environmentDbContext = environmentDbContext;

        }

        [HttpGet]
        //[Authorize]
        public IEnumerable<WeatherForecast> Get()
        {
            //var s = environmentDbContext.EnvironmentalSensor.FirstOrDefault();

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
