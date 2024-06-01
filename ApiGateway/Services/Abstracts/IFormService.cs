using Application.Dtos.Forms;
using BuildingBlocks.Kernel.Services;

namespace ApiGateway.Services.Abstracts;

public interface IFormService : IScopedService
{
    Task<List<BasicFormDto>> GetSubmittableFormsAsync();
}