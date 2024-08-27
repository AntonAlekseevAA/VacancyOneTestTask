namespace VacancyOneTestTask.DataAccess.Entities
{
    public class TicketEntity
    {
        public long Id { get; set; }

        public DateTime Date { get; set; }

        public TicketStatus Status { get; set; }

        public List<AttachedFile> Files { get; set; }
    }
}