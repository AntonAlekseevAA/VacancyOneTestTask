using VacancyOneTestTask.Abstractions;
using VacancyOneTestTask.Abstractions.Models;
using VacancyOneTestTask.DataAccess.Repositories;

namespace VacancyOneTestTask.BL
{
    public class TasksService : ITasksService
    {
        private readonly ITasksRepository _tasksRepository;

        public TasksService(ITasksRepository tasksRepository)
        {
            _tasksRepository = tasksRepository;
        }

        public async Task<long> Create(TicketModel task) => await _tasksRepository.Create(task);

        public async Task Delete(long id) => await _tasksRepository.Delete(id);
        

        public async Task<TicketModel> GetById(long id) => await _tasksRepository.GetById(id);

        public async Task<List<TicketModel>> GetRange(int offset, int limit) => await _tasksRepository.GetRange(offset, limit);

        public async Task Update(long id, UpdateTaskModel task) => await _tasksRepository.Update(id, task);
    }
}