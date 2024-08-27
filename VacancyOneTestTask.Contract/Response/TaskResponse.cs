
namespace VacancyOneTestTask.Contract.Response
{
    // todo move contract
    public class TaskResponse
    {
        public long Id { get; set; }

        public DateTime Date { get; set; }

        public TicketStatus Status { get; set; }

        public List<AttachedFileModel> Files { get; set; } = new List<AttachedFileModel>();
    }
}