namespace VacancyOneTestTask.Abstractions.Models
{
    public class UpdateTaskModel
    {
        public DateTime Date { get; set; }

        public TicketStatus Status { get; set; }

        public List<AttachedFile> Files { get; set; } = new List<AttachedFile>();
    }
}