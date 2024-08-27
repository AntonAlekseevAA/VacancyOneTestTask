using Microsoft.EntityFrameworkCore;
using VacancyOneTestTask.Abstractions.Models;
using VacancyOneTestTask.DataAccess.Entities;

namespace VacancyOneTestTask.DataAccess.Repositories
{
    public class TasksRepository : ITasksRepository
    {
        private readonly VacancyOneDbContext _dbContext;

        public TasksRepository(VacancyOneDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<long> Create(TicketModel task)
        {
            TicketEntity taskEntity = new()
            {
                Date = task.Date,
                Status = (Entities.TicketStatus)task.Status,
                Files = task.Files.Select(f => new Entities.AttachedFile { Url = f.Url }).ToList()
            };
            _dbContext.Tasks.Add(taskEntity);
            await _dbContext.SaveChangesAsync();
            return taskEntity.Id;
        }

        public async Task<TicketModel> GetById(long id)
        {
            var task = await _dbContext.Tasks
                .Include(t => t.Files)
                .SingleAsync(t => t.Id == id);

            var result = new TicketModel
            {
                Id = task.Id,
                Date = task.Date,
                Status = (Abstractions.Models.TicketStatus)task.Status,
                Files = task.Files.Select(f => new Abstractions.Models.AttachedFile{ Url = f.Url }).ToList()
            };
            return result;
        }

        public async Task<List<TicketModel>> GetRange(int offset, int limit)
        {
            var entities = await _dbContext.Tasks
                .Include(t => t.Files)
                .OrderBy(t => t.Id)
                .Skip(offset)
                .Take(limit)
                .ToListAsync();
            return entities.Select(e => new TicketModel
            {
                Id = e.Id,
                Date = e.Date,
                Status = (Abstractions.Models.TicketStatus)e.Status,
                Files = e.Files.Select(f => new Abstractions.Models.AttachedFile { Url = f.Url }).ToList()
            }).ToList();
        }

        public async Task Update(long id, UpdateTaskModel task)
        {
            var entity = await _dbContext.Tasks.SingleAsync(t => t.Id == id);

            entity.Date = task.Date;
            entity.Status = (Entities.TicketStatus)task.Status;
            entity.Files = task.Files.Select(t => new Entities.AttachedFile { Url = t.Url }).ToList();

            _dbContext.Tasks.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var entity = await _dbContext.Tasks.SingleAsync(t => t.Id == id);
            _dbContext.Tasks.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}