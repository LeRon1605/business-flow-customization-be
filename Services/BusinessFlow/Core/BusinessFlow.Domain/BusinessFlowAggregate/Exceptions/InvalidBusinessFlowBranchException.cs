using BuildingBlocks.Domain.Exceptions.Resources;
using Domain;

namespace BusinessFlow.Domain.BusinessFlowAggregate.Exceptions;

public class InvalidBusinessFlowBranchException : ResourceInvalidOperationException
{
    public InvalidBusinessFlowBranchException() : base($"Invalid business flow branch", ErrorCodes.InvalidBusinessFlowBranch)
    {
    }
}