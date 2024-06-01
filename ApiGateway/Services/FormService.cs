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

        if (!spaceIds.Any())
            return new List<SubmittableFormDto>();
        
        var forms = await _submissionClient.GetFormsBySpacesAsync(spaceIds);
        return forms.Select(f => new SubmittableFormDto
        {
            Id = f.Id,
            Name = f.Name,
            SpaceName = spaces.First(s => s.Id == f.SpaceId).Name,
            SpaceColor = spaces.First(s => s.Id == f.SpaceId).Color,
            SpaceId = f.SpaceId,
            VersionId = f.VersionId
        }).ToList();
    }

    public async Task<List<InChargeSubmissionDto>> GetInChargeSubmissionsAsync()
    {
        var inChargeSubmissions = await _businessFlowClient.GetAssignedSubmissionExecutionsAsync();
        var submissionIds = inChargeSubmissions.Select(s => s.SubmissionId).ToList();
        
        if (!submissionIds.Any())
            return new List<InChargeSubmissionDto>();
        
        var submissions = await _submissionClient.GetSubmissionDataAsync(submissionIds);
        return submissions.Select(s =>
        {
            var execution = inChargeSubmissions.First(e => e.SubmissionId == s.Id);
            
            return new InChargeSubmissionDto
            {
                Id = s.Id,
                Name = s.Name,
                SpaceId = execution.SpaceId,
                SpaceName = execution.SpaceName,
                SpaceColor = execution.SpaceColor,
                BusinessFlowId = execution.BusinessFlowId,
                BusinessFlowName = execution.BusinessFlowName,
                FormVersionId = s.FormVersionId
            };
        }).ToList();
    }
}