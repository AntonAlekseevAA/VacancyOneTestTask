using Microsoft.AspNetCore.Http;

namespace VacancyOneTestTask.Contract.Request
{
    public class CreateTaskRequest
    {
        public DateTime Date { get; set; }

        public TicketStatus Status { get; set; }

        public List<IFormFile> Files { get; set; } = new List<IFormFile>();
    }
}