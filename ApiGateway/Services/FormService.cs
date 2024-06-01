using ApiGateway.Clients.Abstracts;
using ApiGateway.Services.Abstracts;
using Application.Dtos.Forms;

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

    public async Task<List<BasicFormDto>> GetSubmittableFormsAsync()
    {
        var spaces = await _businessFlowClient.GetSpacesAsync();
        var spaceIds = spaces.Select(s => s.Id).ToList();
        
        return await _submissionClient.GetFormsBySpacesAsync(spaceIds);
    }
}