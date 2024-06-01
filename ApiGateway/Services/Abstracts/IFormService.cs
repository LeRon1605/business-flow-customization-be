using ApiGateway.Dtos;
using BuildingBlocks.Kernel.Services;

namespace ApiGateway.Services.Abstracts;

public interface IFormService : IScopedService
{
    Task<List<SubmittableFormDto>> GetSubmittableFormsAsync();
    
    Task<List<InChargeSubmissionDto>> GetInChargeSubmissionsAsync();
}