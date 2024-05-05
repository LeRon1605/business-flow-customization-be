using BuildingBlocks.Domain.Exceptions.Resources;
using Domain;

namespace BusinessFlow.Domain.SpaceAggregate.Exceptions;

public class SpaceBusinessFlowAlreadyCreatedException : ResourceInvalidOperationException
{
    public SpaceBusinessFlowAlreadyCreatedException(int id) 
        : base($"Space with id {id} has already created business flow!", ErrorCodes.SpaceAlreadyCreatedBusinessFlow)
    {
    }
}