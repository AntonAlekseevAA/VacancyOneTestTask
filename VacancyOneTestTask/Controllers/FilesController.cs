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
        private IMediator _mediator;

        public FilesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{fileName}")]
        public async Task<IActionResult> Index([FromRoute] string fileName)
        {
            var result = await _mediator.Send(new GetFileRequest { FileName = fileName });
            return File(result, ContentType);
        }
    }
}