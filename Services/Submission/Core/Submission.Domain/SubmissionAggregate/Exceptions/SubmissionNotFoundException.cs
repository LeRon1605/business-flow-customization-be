using BuildingBlocks.Domain.Exceptions.Resources;
using Domain;
using Submission.Domain.SubmissionAggregate.Entities;

namespace Submission.Domain.SubmissionAggregate.Exceptions;

public class SubmissionNotFoundException : ResourceNotFoundException
{
    public SubmissionNotFoundException(int id) 
        : base(nameof(FormSubmission.Id), nameof(FormSubmission.Id), id.ToString(), ErrorCodes.SubmissionNotFound)
    {
    }
    
    public SubmissionNotFoundException(string trackingToken) 
        : base(nameof(FormSubmission.TrackingToken), nameof(FormSubmission.TrackingToken), trackingToken, ErrorCodes.SubmissionNotFound)
    {
    }
}