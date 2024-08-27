using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VacancyOneTestTask.Abstractions;
using VacancyOneTestTask.Abstractions.Models;
using VacancyOneTestTask.Contract;
using VacancyOneTestTask.Contract.Request;
using VacancyOneTestTask.Contract.Response;
using VacancyOneTestTask.DataAccess;
using VacancyOneTestTask.DataAccess.Entities;

namespace VacancyOneTestTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ITasksService _tasksService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ITasksService tasksService)
        {
            _logger = logger;
            this._tasksService = tasksService;
        }

        [HttpPut("create")]
        public async Task<long> CreateTask(CreateTaskRequest request)
        {
            var task = new TicketModel
            {
                Date = request.Date, Status = (Abstractions.Models.TicketStatus)request.Status,
                Files = request.Files.Select(f => new Abstractions.Models.AttachedFile { Url = f.Url }).ToList()
            };
            var taskId = await _tasksService.Create(task);
            return taskId;
        }

        [HttpGet("{id}")]
        public async Task<TaskResponse> GetById([FromRoute] long id)
        {
            var task = await _tasksService.GetById(id);

            var response = new TaskResponse
            {
                Date = task.Date, Id = task.Id, Status = (Contract.TicketStatus)task.Status,
                Files = task.Files.Select(f => new AttachedFileModel { Url = f.Url }).ToList()
            };
            return response;
        }

        [HttpGet("getRange")]
        public async Task<List<TaskResponse>> GetRange([FromQuery] GetTasksRequest request)
        {
            var tasks = await _tasksService.GetRange(request.Offset, request.Limit);
            return tasks.Select(t => new TaskResponse
            {
                Id = t.Id, Date = t.Date, Status = (Contract.TicketStatus)t.Status,
                Files = t.Files.Select(f => new Contract.AttachedFileModel { Url = f.Url }).ToList() }).ToList();
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Update([FromRoute] long id, [FromBody] UpdateTaskRequest request)
        {
            var task = new UpdateTaskModel
            {
                Date = request.Date,
                Status = (Abstractions.Models.TicketStatus)request.Status,
                Files = request.Files.Select(f => new Abstractions.Models.AttachedFile { Url = f.Url }).ToList()
            };

            await _tasksService.Update(id, task);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            await _tasksService.Delete(id);
            return Ok();
        }
    }
}