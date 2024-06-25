using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Identity;
using BuildingBlocks.Domain.Repositories;
using BusinessFlow.Application.UseCases.Spaces.Dtos;
using BusinessFlow.Domain.SpaceAggregate.Entities;
using BusinessFlow.Domain.SpaceAggregate.Exceptions;
using BusinessFlow.Domain.SpaceAggregate.Specifications;

namespace BusinessFlow.Application.UseCases.Spaces.Queries;

public class GetSpaceDetailQueryHandler : IQueryHandler<GetSpaceDetailQuery, SpaceDetailDto>
{
    private readonly IBasicReadOnlyRepository<Space, int> _spaceRepository;
    private readonly IBasicReadOnlyRepository<SpacePermission, int> _spacePermissionRepository;
    private readonly ICurrentUser _currentUser;
    
    public GetSpaceDetailQueryHandler(IBasicReadOnlyRepository<Space, int> spaceRepository, ICurrentUser currentUser, IBasicReadOnlyRepository<SpacePermission, int> spacePermissionRepository)
    {
        _spaceRepository = spaceRepository;
        _currentUser = currentUser;
        _spacePermissionRepository = spacePermissionRepository;
    }
    
    public async Task<SpaceDetailDto> Handle(GetSpaceDetailQuery request, CancellationToken cancellationToken)
    {
        var space = await _spaceRepository.FindByIdAsync(request.Id, new SpaceDetailDto());
        if (space == null)
        {
            throw new SpaceNotFoundException(request.Id);
        }
        var role = space.Members.FirstOrDefault(m => m.Id == _currentUser.Id)!.Role.Id;
        
        var permissions = await _spacePermissionRepository.FilterAsync(new SpacePermissionSpecification(role));
        var userPermissions = permissions.Select(p => p.Name).ToList();

        space.UserPermissions = userPermissions;
        
        return space;
    }
}