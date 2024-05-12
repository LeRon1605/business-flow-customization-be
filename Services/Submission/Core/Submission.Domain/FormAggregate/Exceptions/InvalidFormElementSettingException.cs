using BuildingBlocks.Domain.Exceptions.Resources;
using Domain;

namespace Submission.Domain.FormAggregate.Exceptions;

public class InvalidFormElementSettingException : ResourceInvalidDataException
{
    public InvalidFormElementSettingException() : base("Invalid form element setting", ErrorCodes.InvalidFormElementSetting)
    {
    }
}