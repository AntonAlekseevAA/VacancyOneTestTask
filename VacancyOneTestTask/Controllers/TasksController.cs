using MediatR;
using Microsoft.AspNetCore.Mvc;
using VacancyOneTestTask.Abstractions;
using VacancyOneTestTask.Abstractions.Models;
using VacancyOneTestTask.BL.Handlers;
using VacancyOneTestTask.Contract;
using VacancyOneTestTask.Contract.Request;
using VacancyOneTestTask.Contract.Response;

namespace VacancyOneTestTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITasksService _tasksService;
        private readonly IMediator _mediator;

        public TasksController(ITasksService tasksService, IFileStorageService fileStorageService, IMediator mediator)
        {
            _tasksService = tasksService;
            _mediator = mediator;
        }

        [HttpPut("create")]
        [RequestSizeLimit(long.MaxValue)]
        [DisableRequestSizeLimit]
        public async Task<long> CreateTask(CreateTaskRequest request)
        {
            var createTaskRequest = new CreateTaskOperationRequest
            {
                Date = request.Date, Status = (Abstractions.Models.TicketStatus)request.Status, Files = request.Files
            };

            var result = await _mediator.Send(createTaskRequest);
            return result;
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
                Files = t.Files.Select(f => new AttachedFileModel { Url = f.Url }).ToList()
            }).ToList();
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