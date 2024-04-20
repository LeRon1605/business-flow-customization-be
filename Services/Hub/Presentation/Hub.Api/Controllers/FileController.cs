using Hub.Application.UseCases;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hub.Api.Controllers;

[Route("api/files")]
[ApiController]
public class FileController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public FileController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<IActionResult> UploadFileAsync([FromForm] IFormFile file)
    {
        var url = await _mediator.Send(new UploadFileCommand(file));
        return Ok(new
        {
            Url = url
        });
    }
}