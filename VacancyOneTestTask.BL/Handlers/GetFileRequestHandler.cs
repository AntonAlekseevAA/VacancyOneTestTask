using MediatR;
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

        public GetFileRequestHandler(IFileStorageService fileStorageService)
        {
            _fileStorageService = fileStorageService;
        }

        public async Task<byte[]> Handle(GetFileRequest request, CancellationToken cancellationToken)
        {
            var result = await _fileStorageService.Get(request.FileName);
            return result;
        }
    }
}