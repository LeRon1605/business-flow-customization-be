
using BuildingBlocks.Domain.Exceptions.Resources;
using Domain;

namespace BusinessFlow.Domain.SpaceAggregate.Exceptions;

public class SpaceMemberNotFoundException : ResourceNotFoundException
{
    public SpaceMemberNotFoundException(string id) : base($"Member with id {id} not exist in space!",  ErrorCodes.SpaceMemberNotFound)
    {
    }
}