using BuildingBlocks.Application.Cqrs;
using BusinessFlow.Domain.SubmissionExecutionAggregate.Enums;

namespace BusinessFlow.Application.UseCases.BusinessFlows.Commands;

public class UpdateExecutionTaskStatusCommand : ICommand
{
    public int TaskId { get; set; }
    
    public int ExecutionId { get; set; }
    
    public SubmissionExecutionTaskStatus Status { get; set; }
    
    public UpdateExecutionTaskStatusCommand(int taskId, int executionId, SubmissionExecutionTaskStatus status)
    {
        TaskId = taskId;
        ExecutionId = executionId;
        Status = status;
    }
}