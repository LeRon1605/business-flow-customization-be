using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Dtos;
using BuildingBlocks.Domain.Specifications.Interfaces;
using BusinessFlow.Application.UseCases.Spaces.Dtos;
using BusinessFlow.Domain.SpaceAggregate.DomainServices;
using BusinessFlow.Domain.SpaceAggregate.Entities;
using BusinessFlow.Domain.SpaceAggregate.Exceptions;
using BusinessFlow.Domain.SpaceAggregate.Repositories;

namespace BusinessFlow.Application.UseCases.Spaces.Queries;

public class GetAllMembersInSpaceQueryHandler : IQueryHandler<GetAllMembersInSpaceQuery, PagedResultDto<SpaceMemberDto>>
{
    private readonly ISpaceRepository _spaceRepository;
    
    public GetAllMembersInSpaceQueryHandler(ISpaceRepository spaceRepository)
    {
        _spaceRepository = spaceRepository;
    }
    
    public async Task<PagedResultDto<SpaceMemberDto>> Handle(GetAllMembersInSpaceQuery request, CancellationToken cancellationToken)
    {
        var space = await _spaceRepository.FindByIdAsync(request.SpaceId, new SpaceDetailDto());
        if (space == null)
        {
            throw new SpaceNotFoundException(request.SpaceId);
        }
        
        var total = space.Members.Count;
        var startIndex = (request.Page - 1) * request.Size;
        var members = new List<SpaceMemberDto>();

        if (startIndex < total)
        {
            var count = Math.Min(request.Size, total - startIndex);
            members = space.Members.GetRange(startIndex, count);
        }
        else
        {
            members = new List<SpaceMemberDto>();
        }

        return new PagedResultDto<SpaceMemberDto>(total, request.Size, members);

    }
}