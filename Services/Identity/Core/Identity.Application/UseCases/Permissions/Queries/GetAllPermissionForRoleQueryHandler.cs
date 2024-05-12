using Application.Dtos;
using Application.Dtos.Submissions.Identity;
using AutoMapper;
using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Domain.Repositories;
using Identity.Application.UseCases.Permissions.Dtos;
using Identity.Domain.PermissionAggregate.Entities;
using Identity.Domain.PermissionAggregate.Specifications;
using Identity.Domain.RoleAggregate;
using Identity.Domain.RoleAggregate.Exceptions;

namespace Identity.Application.UseCases.Permissions.Queries;

public class GetAllPermissionForRoleQueryHandler : IQueryHandler<GetAllPermissionForRoleQuery, IEnumerable<PermissionDto>>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IBasicReadOnlyRepository<ApplicationPermission, int> _permissionRepository;
    private readonly IMapper _mapper;

    public GetAllPermissionForRoleQueryHandler(IRoleRepository roleRepository
        , IBasicReadOnlyRepository<ApplicationPermission, int> permissionRepository
        , IMapper mapper)
    {
        _roleRepository = roleRepository;
        _permissionRepository = permissionRepository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<PermissionDto>> Handle(GetAllPermissionForRoleQuery request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.FindByIdAsync(request.RoleId);
        if (role == null)
        {
            throw new RoleNotFoundException(request.RoleId);
        }

        var specification = new PermissionInSpecification(role.Permissions.Select(x => x.PermissionId)).And(
            new PermissionByNameSpecification(request.Name));
        
        var permissions = await _permissionRepository.FilterAsync(specification);

        return _mapper.Map<IEnumerable<PermissionDto>>(permissions);
    }
}