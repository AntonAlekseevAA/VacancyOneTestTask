namespace VacancyOneTestTask.Contract
{
    /// <summary>
    /// Чтобы не дублировать модель в <see cref="VacancyOneTestTask.Contract.Request.CreateTaskRequest"/>
    /// оставил только url - этого достаточно, чтобы получить загруженные файлы, а другие атрибуты не используются
    /// </summary>
    public class AttachedFileModel
    {
        public string Url { get; set; } = null!;
    }
}