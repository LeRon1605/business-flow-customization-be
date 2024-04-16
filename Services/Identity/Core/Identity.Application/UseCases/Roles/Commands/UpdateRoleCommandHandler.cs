using AutoMapper;
using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Domain.Exceptions.Resources;
using Identity.Application.UseCases.Roles.Dtos;
using Identity.Domain.RoleAggregate.Entities;
using Identity.Domain.RoleAggregate.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Identity.Application.UseCases.Roles.Commands;

public class UpdateRoleCommandHandler : ICommandHandler<UpdateRoleCommand, RoleDto>
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IMapper _mapper; 

    public UpdateRoleCommandHandler(RoleManager<ApplicationRole> roleManager
        , IMapper mapper)
    {
        _roleManager = roleManager;
        _mapper = mapper;
    }
    
    public async Task<RoleDto> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleManager.FindByIdAsync(request.Id);
        if (role == null)
        {
            throw new RoleNotFoundException(request.Id);
        }
        
        role.Update(request.Name);
        
        var result = await _roleManager.UpdateAsync(role);
        if (!result.Succeeded)
        {
            var error = result.Errors.First();
            throw new ResourceInvalidOperationException(error.Description, error.Code);

        }

        return _mapper.Map<RoleDto>(role);
    }
}