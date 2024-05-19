using BuildingBlocks.Domain.Exceptions.Resources;
using BusinessFlow.Domain.SubmissionExecutionAggregate.Entities;
using Domain;

namespace BusinessFlow.Domain.SubmissionExecutionAggregate.Exceptions;

public class ExecutionNotFoundException : ResourceNotFoundException
{
    public ExecutionNotFoundException(int executionId) 
        : base(nameof(SubmissionExecution), nameof(SubmissionExecution.Id), ErrorCodes.ExecutionNotFound)
    {
    }
}