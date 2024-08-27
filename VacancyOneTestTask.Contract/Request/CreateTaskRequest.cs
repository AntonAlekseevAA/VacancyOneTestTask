namespace VacancyOneTestTask.Contract.Request
{
    /// <summary>
    /// todo move
    /// </summary>
    public class CreateTaskRequest
    {
        public DateTime Date { get; set; }

        public TicketStatus Status { get; set; }

        public List<AttachedFileModel> Files { get; set; } = new List<AttachedFileModel>();
    }
}