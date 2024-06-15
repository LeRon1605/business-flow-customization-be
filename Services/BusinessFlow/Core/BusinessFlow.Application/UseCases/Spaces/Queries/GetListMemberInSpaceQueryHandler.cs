using AutoMapper;
using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Domain.Repositories;
using BusinessFlow.Application.UseCases.Spaces.Dtos;
using BusinessFlow.Domain.SpaceAggregate.Entities;
using BusinessFlow.Domain.SpaceAggregate.Exceptions;
using BusinessFlow.Domain.SpaceAggregate.Repositories;

namespace BusinessFlow.Application.UseCases.Spaces.Queries;

public class GetListMemberInSpaceQueryHandler : IQueryHandler<GetListMemberInSpaceQuery, List<SpaceMemberDto>>
{
    private readonly ISpaceRepository _spaceRepository;
    
    public GetListMemberInSpaceQueryHandler(ISpaceRepository spaceRepository)
    {
        _spaceRepository = spaceRepository;
    }
    
    public async Task<List<SpaceMemberDto>> Handle(GetListMemberInSpaceQuery request, CancellationToken cancellationToken)
    {
        var space = await _spaceRepository.FindByIdAsync(request.SpaceId, $"{nameof(Space.Members)}.{nameof(SpaceMember.Role)}");
        if (space == null)
        {
            throw new SpaceNotFoundException(request.SpaceId);
        }
        
        return space.Members.Select(m => new SpaceMemberDto
        {
            Id = m.UserId,
            Role = new SpaceRoleDto
            {
                Id = m.Role.Id,
                Name = m.Role.Name
            }
        }).ToList();
    }
}