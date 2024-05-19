using BuildingBlocks.Domain.Exceptions.Resources;
using Domain;

namespace BusinessFlow.Domain.SubmissionExecutionAggregate.Exceptions;

public class SubmissionExecutionNotFoundException : ResourceNotFoundException
{
    public SubmissionExecutionNotFoundException(int submissionId) 
        : base($"Execution for submission {submissionId} not found", ErrorCodes.SubmissionExecutionNotFound)
    {
    }
}