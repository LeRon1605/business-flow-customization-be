using BuildingBlocks.Domain.Exceptions.Resources;
using Domain;

namespace BusinessFlow.Domain.SubmissionExecutionAggregate.Exceptions;

public class SubmissionHasAlreadyExecutedException : ResourceInvalidOperationException
{
    public SubmissionHasAlreadyExecutedException(int submissionId, Guid blockId) 
        : base($"Submission {submissionId} has already executed on block {blockId}", ErrorCodes.SubmissionHasAlreadyExecuted)
    {
    }
}