using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using VacancyOneTestTask.Abstractions;
using VacancyOneTestTask.Abstractions.Models;

namespace VacancyOneTestTask.BL.Handlers
{
    public class CreateTaskOperationRequest : IRequest<long>
    {
        public DateTime Date { get; set; }

        public TicketStatus Status { get; set; }

        public List<IFormFile> Files { get; set; } = new List<IFormFile>();
    }

    public class CreateTaskRequestHandler : IRequestHandler<CreateTaskOperationRequest, long>
    {
        private readonly IFileStorageService _fileStorageService;
        private readonly ILogger<CreateTaskRequestHandler> _logger;
        private readonly ITasksService _tasksService;

        public CreateTaskRequestHandler(IFileStorageService fileStorageService, ILogger<CreateTaskRequestHandler> logger, ITasksService tasksService)
        {
            _fileStorageService = fileStorageService;
            _logger = logger;
            _tasksService = tasksService;
        }

        public async Task<long> Handle(CreateTaskOperationRequest request, CancellationToken cancellationToken)
        {
            var task = new TicketModel
            {
                Date = request.Date,
                Status = request.Status,
                Files = request.Files.Select(f => new AttachedFile { Url = f.FileName }).ToList()
            };

            for (int i = 0; i < request.Files.Count; i++)
            {
                IFormFile? file = request.Files[i];
                try
                {
                    using (var contentStream = file.OpenReadStream())
                    {
                        await _fileStorageService.Put(contentStream, file.FileName);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Some error occuried while saving files, perform rollback");
                    for (var j = 0; j < i; j++)
                    {
                        await _fileStorageService.Delete(request.Files[j].FileName);
                    }

                    throw;
                }
                finally
                {
                    _logger.LogInformation("Create task request with and {FilesCount} was processed", request.Files.Count);
                }
            }

            try
            {
                var taskId = await _tasksService.Create(task);
                return taskId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create task entity. Perform rollback");

                foreach (var file in request.Files)
                {
                    await _fileStorageService.Delete(file.FileName);
                }

                throw;
            }
        }
    }
}