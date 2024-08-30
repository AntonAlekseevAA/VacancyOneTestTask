using MediatR;
using Microsoft.AspNetCore.Mvc;
using VacancyOneTestTask.BL.Handlers;

namespace VacancyOneTestTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilesController : ControllerBase
    {
        private const string ContentType = "application/json";
        private readonly IMediator _mediator;

        public FilesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{fileName}")]
        public async Task<IActionResult> DownloadFile([FromRoute] string fileName)
        {
            var result = await _mediator.Send(new GetFileRequest { FileName = fileName });
            return File(result, ContentType);
        }

        [HttpDelete("{fileName}")]
        public async Task<IActionResult> Delete([FromRoute] string fileName)
        {
            await _mediator.Send(new DeleteFileRequest { FileName = fileName });
            return Ok();
        }
    }
}