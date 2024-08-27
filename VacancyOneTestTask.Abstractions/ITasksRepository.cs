using VacancyOneTestTask.Abstractions.Models;

namespace VacancyOneTestTask.DataAccess.Repositories
{
    public interface ITasksRepository
    {
        Task<long> Create(TicketModel task);
        Task Delete(long id);
        Task<TicketModel> GetById(long id);
        Task<List<TicketModel>> GetRange(int offset, int limit);
        Task Update(long id, UpdateTaskModel task);
    }
}