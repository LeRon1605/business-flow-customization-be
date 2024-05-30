using BuildingBlocks.Domain.Exceptions.Resources;
using Domain;

namespace BusinessFlow.Domain.SubmissionExecutionAggregate.Exceptions;

public class ExecutionHasAlreadyCompletedException : ResourceInvalidOperationException
{
    public ExecutionHasAlreadyCompletedException(int executionId) 
        : base($"Execution {executionId} has already completed", ErrorCodes.ExecutionHasAlreadyCompleted)
    {
    }
}