using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Dtos;
using BuildingBlocks.Domain.Specifications.Interfaces;
using BusinessFlow.Application.UseCases.Spaces.Dtos;
using BusinessFlow.Domain.SpaceAggregate.DomainServices;
using BusinessFlow.Domain.SpaceAggregate.Exceptions;
using BusinessFlow.Domain.SpaceAggregate.Repositories;

namespace BusinessFlow.Application.UseCases.Spaces.Queries;

public class GetAllMembersInSpaceQueryHandler : IQueryHandler<GetAllMembersInSpaceQuery, List<SpaceMemberDto>>
{
    private readonly ISpaceRepository _spaceRepository;
    
    public GetAllMembersInSpaceQueryHandler(ISpaceRepository spaceRepository)
    {
        _spaceRepository = spaceRepository;
    }
    
    public async Task<List<SpaceMemberDto>> Handle(GetAllMembersInSpaceQuery request, CancellationToken cancellationToken)
    {
        var space = await _spaceRepository.FindByIdAsync(request.SpaceId, new SpaceDetailDto());
        if (space == null)
        {
            throw new SpaceNotFoundException(request.SpaceId);
        }
        
        return space.Members;
    }
}