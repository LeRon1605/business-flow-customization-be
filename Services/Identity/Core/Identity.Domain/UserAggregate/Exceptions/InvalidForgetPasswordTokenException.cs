using BuildingBlocks.Domain.Exceptions.Resources;

namespace Identity.Domain.UserAggregate.Exceptions;

public class InvalidForgetPasswordTokenException : ResourceInvalidOperationException
{
    public InvalidForgetPasswordTokenException() 
        : base("Token is invalid!", ErrorCodes.InvalidForgetPasswordToken)
    {
    }
}