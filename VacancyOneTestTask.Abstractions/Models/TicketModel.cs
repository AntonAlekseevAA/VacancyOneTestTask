namespace VacancyOneTestTask.Abstractions.Models
{
    public class TicketModel
    {
        public long Id { get; set; }

        public DateTime Date { get; set; }

        public TicketStatus Status { get; set; }

        public List<AttachedFile> Files { get; set; } = new List<AttachedFile>();
    }
}