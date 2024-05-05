using BuildingBlocks.Domain.Exceptions.Resources;
using BusinessFlow.Domain.SpaceAggregate.Entities;
using Domain;

namespace BusinessFlow.Domain.SpaceAggregate.Exceptions;

public class SpaceMemberAlreadyExistedException : ResourceAlreadyExistException
{
    public SpaceMemberAlreadyExistedException(string userId) : base(nameof(SpaceMember), nameof(SpaceMember), userId, ErrorCodes.SpaceMemberAlreadyExisted)
    {
    }
}