using VacancyOneTestTask.DataAccess.Entities;

namespace VacancyOneTestTask.Controllers
{
    /// <summary>
    /// todo move
    /// </summary>
    public class CreateTaskRequest
    {
        public DateTime Date { get; set; }

        public TicketStatus Status { get; set; }

        // todo files
    }
}