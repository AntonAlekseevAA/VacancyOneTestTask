using VacancyOneTestTask.DataAccess.Entities;

namespace VacancyOneTestTask.Controllers
{
    // todo move
    internal class CreateTaskResponse
    {
        public long Id { get; set; }

        public DateTime Date { get; set; }

        public TicketStatus Status { get; set; }

        public List<AttachedFileModel> Files { get; set; } = null!;
    }
}