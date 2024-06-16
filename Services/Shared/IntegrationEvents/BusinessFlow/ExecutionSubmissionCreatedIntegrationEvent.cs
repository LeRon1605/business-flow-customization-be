using BuildingBlocks.EventBus;

namespace IntegrationEvents.BusinessFlow;

public class ExecutionSubmissionCreatedIntegrationEvent : IntegrationEvent
{
    public int ExecutionId { get; set; }
    
    public int SubmitId { get; set; }
    
    public string Name { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public Guid BusinessFlowBlockId { get; set; }
    
    public ExecutionSubmissionCreatedIntegrationEvent(int executionId
        , int submitId
        , string name
        , DateTime createdAt
        , Guid businessFlowBlockId)
    {
        ExecutionId = executionId;
        SubmitId = submitId;
        Name = name;
        BusinessFlowBlockId = businessFlowBlockId;
        CreatedAt = createdAt;
    }
}