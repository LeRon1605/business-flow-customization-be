using AutoMapper;
using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Domain.Exceptions.Resources;
using Identity.Application.UseCases.Roles.Dtos;
using Identity.Domain.RoleAggregate.Entities;
using Microsoft.AspNetCore.Identity;

namespace Identity.Application.UseCases.Roles.Commands;

public class CreateRoleCommandHandler : ICommandHandler<CreateRoleCommand, RoleDto>
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IMapper _mapper; 

    public CreateRoleCommandHandler(RoleManager<ApplicationRole> roleManager
        , IMapper mapper)
    {
        _roleManager = roleManager;
        _mapper = mapper;
    }
    
    public async Task<RoleDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = new ApplicationRole(request.Name);
        
        var result = await _roleManager.CreateAsync(role);
        if (!result.Succeeded)
        {
            var error = result.Errors.First();
            throw new ResourceInvalidOperationException(error.Description, error.Code);
        }
        
        return _mapper.Map<RoleDto>(role);
    }
}