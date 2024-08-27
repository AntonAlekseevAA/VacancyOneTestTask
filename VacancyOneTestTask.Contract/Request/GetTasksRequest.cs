namespace VacancyOneTestTask.Controllers
{
    public class GetTasksRequest
    {
        public int Offset { get; set; }

        public int Limit { get; set; } = 10;
    }
}