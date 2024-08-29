using MediatR;
using Microsoft.Extensions.Logging;
using VacancyOneTestTask.Abstractions;

namespace VacancyOneTestTask.BL.Handlers
{
    public class GetFileRequest : IRequest<byte[]>
    {
        public string FileName { get; set; } = null!;
    }

    public class GetFileRequestHandler : IRequestHandler<GetFileRequest, byte[]>
    {
        private readonly IFileStorageService _fileStorageService;
        private readonly ILogger<GetFileRequestHandler> _logger;

        public GetFileRequestHandler(IFileStorageService fileStorageService, ILogger<GetFileRequestHandler> logger)
        {
            _fileStorageService = fileStorageService;
            _logger = logger;
        }

        public async Task<byte[]> Handle(GetFileRequest request, CancellationToken cancellationToken)
        {
            var result = await _fileStorageService.Get(request.FileName);
            return result;
        }
    }
}