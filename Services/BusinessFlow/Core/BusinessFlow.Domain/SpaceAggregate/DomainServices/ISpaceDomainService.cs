using BuildingBlocks.Domain.Services;
using BusinessFlow.Domain.SpaceAggregate.Entities;

namespace BusinessFlow.Domain.SpaceAggregate.DomainServices;

public interface ISpaceDomainService : IDomainService
{
    Task<Space> CreateAsync(string name, string description, string color, string creatorId);
    Task<Space> UpdateBasicInfoAsync(Space space, string name, string description, string color);
}