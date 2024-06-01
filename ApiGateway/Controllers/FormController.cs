using ApiGateway.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers;

[ApiController]
[Route("api/forms")]
public class FormController : ControllerBase
{
    private readonly IFormService _formService;
    
    public FormController(IFormService formService)
    {
        _formService = formService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetForms()
    {
        var forms = await _formService.GetSubmittableFormsAsync();
        return Ok(forms);
    }
}