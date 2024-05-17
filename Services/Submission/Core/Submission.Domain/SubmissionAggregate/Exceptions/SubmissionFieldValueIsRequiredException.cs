using BuildingBlocks.Domain.Exceptions.Resources;
using Domain;

namespace Submission.Domain.SubmissionAggregate.Exceptions;

public class SubmissionFieldValueIsRequiredException : ResourceInvalidDataException
{
    public SubmissionFieldValueIsRequiredException(int elementId) 
        : base($"Element {elementId} is required!", ErrorCodes.SubmissionFieldValueIsRequired)
    {
    }
}