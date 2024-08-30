using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using VacancyOneTestTask.Abstractions;

namespace VacancyOneTestTask.DataAccess
{
    public class FileStorageService : IFileStorageService
    {
        private const int BufferSize = 4096;
        private readonly ILogger<FileStorageService> _logger;

        public FileStorageService(ILogger<FileStorageService> logger)
        {
            _logger = logger;
        }

        public async Task Put(Stream contentStream, string filename)
        {
            try
            {
                await using var fs = File.Create(filename, BufferSize, FileOptions.Asynchronous);
                await contentStream.CopyToAsync(fs);
                await fs.FlushAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        public async Task Delete(string fileName)
        {
            try
            {
                File.Delete(fileName);
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        public async Task<byte[]> Get(string filename)
        {
            var result = await File.ReadAllBytesAsync(filename);
            return result;
        }
    }
}