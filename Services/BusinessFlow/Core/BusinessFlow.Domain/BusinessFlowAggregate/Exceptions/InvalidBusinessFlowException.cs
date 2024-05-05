using BuildingBlocks.Domain.Exceptions.Resources;
using Domain;

namespace BusinessFlow.Domain.BusinessFlowAggregate.Exceptions;

public class InvalidBusinessFlowException : ResourceInvalidOperationException
{
    public InvalidBusinessFlowException() : base("Invalid business flow", ErrorCodes.InvalidBusinessFlow)
    {
    }
}