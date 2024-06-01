using ApiGateway.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers;

[ApiController]
[Route("api/submissions")]
public class SubmissionController : ControllerBase
{
    private readonly IFormService _formService;
    
    public SubmissionController(IFormService formService)
    {
        _formService = formService;
    }
    
    [HttpGet("in-charge-submissions")]
    public async Task<IActionResult> GetSubmittableForms()
    {
        var submissions = await _formService.GetInChargeSubmissionsAsync();
        return Ok(submissions);
    }
}