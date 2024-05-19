using BuildingBlocks.Domain.Exceptions.Resources;
using Domain;

namespace BusinessFlow.Domain.SubmissionExecutionAggregate.Exceptions;

public class ExecutionInCompletedTaskException : ResourceInvalidOperationException
{
    public ExecutionInCompletedTaskException(int executionId) 
        : base($"Execution {executionId} has in completed tasks", ErrorCodes.ExecutionInCompletedTask)
    {
    }
}