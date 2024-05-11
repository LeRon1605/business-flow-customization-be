using BuildingBlocks.Domain.Exceptions.Resources;
using Domain;

namespace Submission.Domain.SubmissionAggregate.Exceptions;

public class InvalidSubmissionFieldValueException : ResourceInvalidDataException
{
    public InvalidSubmissionFieldValueException() : base("Invalid submission field value!", ErrorCodes.InvalidSubmissionFieldValue)
    {
    }
}