using BusinessFlow.Domain.SpaceAggregate.Entities;
using BusinessFlow.Domain.SpaceAggregate.Exceptions;
using BusinessFlow.Domain.SpaceAggregate.Repositories;
using BusinessFlow.Domain.SpaceAggregate.Specifications;

namespace BusinessFlow.Domain.SpaceAggregate.DomainServices;

public class SpaceDomainService : ISpaceDomainService
{
    private readonly ISpaceRepository _spaceRepository;
    
    public SpaceDomainService(ISpaceRepository spaceRepository)
    {
        _spaceRepository = spaceRepository;
    }
    
    public async Task<Space> CreateAsync(string name, string description, string color, string creatorId)
    {
        await ValidateDuplicateNameAsync(name);
        
        var space = new Space(name, description, color);
        space.AddMember(creatorId, Enums.SpaceRole.Manager);
        
        await _spaceRepository.InsertAsync(space);

        return space;
    }

    public async Task<Space> UpdateBasicInfoAsync(Space space, string name, string description, string color)
    {
        if (space.Name != name)
        {
            await ValidateDuplicateNameAsync(name);
        }
        space.UpdateSpaceBasicInfo(name, description, color);
        _spaceRepository.Update(space);
        return space;
    }

    public async Task AddMemberAsync(Space space, string userId, int role)
    {
        space.AddMember(userId, (Enums.SpaceRole)role);
        _spaceRepository.Update(space);
    }

    private async Task ValidateDuplicateNameAsync(string name)
    {
        var isNameExisted = await _spaceRepository.AnyAsync(new SpaceByNameSpecification(name));
        if (isNameExisted)
        {
            throw new SpaceAlreadyExistedException(name);
        }
    }
}