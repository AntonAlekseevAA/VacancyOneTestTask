namespace VacancyOneTestTask.Abstractions
{
    public interface IFileStorageService
    {
        Task Put(Stream contentStream, string filename);

        Task<byte[]> Get(string filename);

        Task Delete(string fileName);
    }
}