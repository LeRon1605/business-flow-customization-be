using BuildingBlocks.Domain.Exceptions.Resources;
using BusinessFlow.Domain.SpaceAggregate.Entities;
using Domain;

namespace BusinessFlow.Domain.SpaceAggregate.Exceptions;

public class SpaceAlreadyExistedException : ResourceAlreadyExistException
{
    public SpaceAlreadyExistedException(string name) : base(nameof(Space), nameof(Space.Name), name, ErrorCodes.SpaceAlreadyExisted)
    {
    }
}