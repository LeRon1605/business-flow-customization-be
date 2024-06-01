using ApiGateway.Clients.Abstracts;
using ApiGateway.Dtos;
using ApiGateway.Services.Abstracts;

namespace ApiGateway.Services;

public class FormService : IFormService
{
    private readonly ISubmissionClient _submissionClient;
    private readonly IBusinessFlowClient _businessFlowClient;
    
    public FormService(ISubmissionClient submissionClient, IBusinessFlowClient businessFlowClient)
    {
        _submissionClient = submissionClient;
        _businessFlowClient = businessFlowClient;
    }

    public async Task<List<SubmittableFormDto>> GetSubmittableFormsAsync()
    {
        var spaces = await _businessFlowClient.GetSpacesAsync();
        var spaceIds = spaces.Select(s => s.Id).ToList();
        
        var forms = await _submissionClient.GetFormsBySpacesAsync(spaceIds);
        return forms.Select(f => new SubmittableFormDto
        {
            Id = f.Id,
            Name = f.Name,
            SpaceName = spaces.First(s => s.Id == f.SpaceId).Name,
            SpaceColor = spaces.First(s => s.Id == f.SpaceId).Color
        }).ToList();
    }
}