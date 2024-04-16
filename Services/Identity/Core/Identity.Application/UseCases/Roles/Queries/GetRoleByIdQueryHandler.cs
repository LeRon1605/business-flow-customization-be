using AutoMapper;
using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Domain.Repositories;
using Identity.Application.UseCases.Roles.Dtos;
using Identity.Domain.RoleAggregate.Entities;
using Identity.Domain.RoleAggregate.Exceptions;

namespace Identity.Application.UseCases.Roles.Queries;

public class GetRoleByIdQueryHandler : IQueryHandler<GetRoleByIdQuery, RoleDto>
{
    private readonly IBasicReadOnlyRepository<ApplicationRole, string> _roleRepository;
    private readonly IMapper _mapper;
    public GetRoleByIdQueryHandler(IBasicReadOnlyRepository<ApplicationRole, string> roleRepository
        , IMapper mapper)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
    }
    
    public async Task<RoleDto> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.FindByIdAsync(request.Id);
        if (role == null)
        {
            throw new RoleNotFoundException(request.Id);
        }
        
        return _mapper.Map<RoleDto>(role);
    }
}