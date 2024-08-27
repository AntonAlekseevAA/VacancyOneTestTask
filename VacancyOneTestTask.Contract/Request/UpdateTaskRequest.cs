namespace VacancyOneTestTask.Contract.Request
{
    public class UpdateTaskRequest
    {
        public DateTime Date { get; set; }

        public TicketStatus Status { get; set; }

        public List<AttachedFileModel> Files { get; set; }
    }
}