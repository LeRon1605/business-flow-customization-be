using BuildingBlocks.Domain.Exceptions.Resources;
using Domain;
using Submission.Domain.SubmissionAggregate.Entities;

namespace Submission.Domain.SubmissionAggregate.Exceptions;

public class ExecutionSubmissionNotFoundException : ResourceNotFoundException
{
    public ExecutionSubmissionNotFoundException(int id) 
        : base(nameof(FormSubmission.Id), nameof(FormSubmission.ExecutionId), id.ToString(), ErrorCodes.ExecutionSubmissionNotFound)
    {
    }
}