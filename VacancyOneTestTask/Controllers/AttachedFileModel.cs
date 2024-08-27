namespace VacancyOneTestTask.Controllers
{
    internal class AttachedFileModel
    {
        public long Id { get; set; }

        public long TaskId { get; set; }

        public string Url { get; set; } = null!;
    }
}