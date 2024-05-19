using BuildingBlocks.EventBus;

namespace IntegrationEvents.FormSubmissions;

public class FormSubmissionCreatedIntegrationEvent : IntegrationEvent
{
    public int SubmissionId { get; set; }
    
    public int BusinessFlowVersionId { get; set; }
    
    public FormSubmissionCreatedIntegrationEvent(int submissionId, int businessFlowVersionId)
    {
        SubmissionId = submissionId;
        BusinessFlowVersionId = businessFlowVersionId;
    }
}