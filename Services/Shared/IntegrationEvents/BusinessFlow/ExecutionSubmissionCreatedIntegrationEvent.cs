using BuildingBlocks.EventBus;

namespace IntegrationEvents.BusinessFlow;

public class ExecutionSubmissionCreatedIntegrationEvent : IntegrationEvent
{
    public int ExecutionId { get; set; }
    
    public Guid BusinessFlowBlockId { get; set; }
    
    public ExecutionSubmissionCreatedIntegrationEvent(int executionId, Guid businessFlowBlockId)
    {
        ExecutionId = executionId;
        BusinessFlowBlockId = businessFlowBlockId;
    }
}