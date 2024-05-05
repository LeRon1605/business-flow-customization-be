using BuildingBlocks.Domain.Exceptions.Resources;
using BusinessFlow.Domain.SpaceAggregate.Entities;
using Domain;

namespace BusinessFlow.Domain.SpaceAggregate.Exceptions;

public class SpaceNotFoundException : ResourceNotFoundException
{
    public SpaceNotFoundException(int id) : base(nameof(Space), nameof(Space.Id), id.ToString(), ErrorCodes.SpaceNotFound)
    {
    }
}