using Microsoft.AspNetCore.Mvc;
using VacancyOneTestTask.DataAccess;
using VacancyOneTestTask.DataAccess.Entities;

namespace VacancyOneTestTask.Controllers
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
        private readonly VacancyOneDbContext _dbContext;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, VacancyOneDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
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

        [HttpPut("create")]
        public async Task<IActionResult> CreateTask(CreateTaskRequest request)
        {
            var task = new DataAccess.Entities.Task
            {
                Date = request.Date,
                Status = request.Status,
                Files = new List<AttachedFile> { new() { Url = "https://www.example.com" } }
            };
            
            _dbContext.Tasks.Add(task); // todo repo
            await _dbContext.SaveChangesAsync();

            var result = new CreateTaskResponse
            {
                Id = task.Id,
                Date = task.Date,
                Status = task.Status,
                Files = task.Files.Select(f => new AttachedFileModel { Id = f.Id, TaskId = f.TaskId, Url = f.Url }).ToList(),
            };

            return Ok(result); // todo response model
        }
    }
}