using MediatR;
using VacancyOneTestTask.Abstractions;
using VacancyOneTestTask.Abstractions.Models;
using VacancyOneTestTask.BL.Handlers;
using VacancyOneTestTask.DataAccess.Repositories;

namespace VacancyOneTestTask.BL
{
    public class TasksService : ITasksService
    {
        private readonly ITasksRepository _tasksRepository;
        private readonly IMediator _mediator;

        public TasksService(ITasksRepository tasksRepository, IMediator mediator)
        {
            _tasksRepository = tasksRepository;
            _mediator = mediator;
        }

        public async Task<long> Create(TicketModel task) => await _tasksRepository.Create(task);

        public async Task Delete(long id)
        {
            var task = await _tasksRepository.GetById(id);
            foreach (var file in task.Files)
            {
                await _mediator.Send(new DeleteFileRequest { FileName = file.Url });
            }

            await _tasksRepository.Delete(id);
        }


        public async Task<TicketModel> GetById(long id) => await _tasksRepository.GetById(id);

        public async Task<List<TicketModel>> GetRange(int offset, int limit) => await _tasksRepository.GetRange(offset, limit);

        public async Task Update(long id, UpdateTaskModel task) => await _tasksRepository.Update(id, task);
    }
}