using MediatR;
using VacancyOneTestTask.Abstractions;

namespace VacancyOneTestTask.BL.Handlers
{
    public class DeleteFileRequest : IRequest<Unit>
    {
        public string FileName { get; set; } = null!;
    }

    public class DeleteFileRequestHandler : IRequestHandler<DeleteFileRequest, Unit>
    {
        private readonly IFileStorageService _fileStorageService;

        public DeleteFileRequestHandler(IFileStorageService fileStorageService)
        {
            _fileStorageService = fileStorageService;
        }

        public async Task<Unit> Handle(DeleteFileRequest request, CancellationToken cancellationToken)
        {
            await _fileStorageService.Delete(request.FileName);
            return Unit.Value;
        }
    }
}