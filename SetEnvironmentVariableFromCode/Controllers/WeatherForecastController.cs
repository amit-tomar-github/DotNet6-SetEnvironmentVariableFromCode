using Microsoft.AspNetCore.Mvc;

namespace SetEnvironmentVariableFromCode.Controllers
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
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration config, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            _config = config;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public WeatherForecast Get()
        {
            return new WeatherForecast
            {
                //this value is actual environment value
                HostingEnvironement = _hostingEnvironment.EnvironmentName,
                LoginId = _config.GetSection("LoginId").Value,//Or we can use _config.GetValue<string>("LoginId")
                //this value is based on system environment variable or in development will come from launchsetting.json
                EnvName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
            };
        }
    }
}