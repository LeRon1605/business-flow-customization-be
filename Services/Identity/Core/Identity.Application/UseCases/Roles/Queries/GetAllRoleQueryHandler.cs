using AutoMapper;
using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Dtos;
using BuildingBlocks.Domain.Repositories;
using Identity.Application.UseCases.Roles.Dtos;
using Identity.Domain.RoleAggregate.Entities;
using Identity.Domain.RoleAggregate.Specifications;

namespace Identity.Application.UseCases.Roles.Queries;

public class GetAllRoleQueryHandler : IQueryHandler<GetAllRoleQuery, PagedResultDto<RoleDto>>
{
    private readonly IBasicReadOnlyRepository<ApplicationRole, string> _roleRepository;
    private readonly IMapper _mapper;
    
    public GetAllRoleQueryHandler(IBasicReadOnlyRepository<ApplicationRole, string> roleRepository
        , IMapper mapper)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
    }
    
    public async Task<PagedResultDto<RoleDto>> Handle(GetAllRoleQuery request, CancellationToken cancellationToken)
    {
        var specification = new RoleFilterAndPagingSpecification(request.Page, request.Size, request.Sorting, request.Name);
        
        var roles = await _roleRepository.GetPagedListAsync(specification);
        var total = await _roleRepository.GetCountAsync(specification);
        
        return new PagedResultDto<RoleDto>(
            total,
            request.Size, 
            _mapper.Map<IEnumerable<RoleDto>>(roles));
    }
}