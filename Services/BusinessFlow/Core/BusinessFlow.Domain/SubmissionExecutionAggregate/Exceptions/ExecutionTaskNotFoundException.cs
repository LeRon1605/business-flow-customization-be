using BuildingBlocks.Domain.Exceptions.Resources;
using Domain;

namespace BusinessFlow.Domain.SubmissionExecutionAggregate.Exceptions;

public class ExecutionTaskNotFoundException : ResourceNotFoundException
{
    public ExecutionTaskNotFoundException(int executionId, int taskId) 
        : base($"Task {taskId} not found in execution {executionId}", ErrorCodes.ExecutionTaskNotFound)
    {
    }
}