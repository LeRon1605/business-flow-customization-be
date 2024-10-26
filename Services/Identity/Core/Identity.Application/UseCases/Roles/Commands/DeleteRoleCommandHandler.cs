﻿using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Domain.Exceptions.Resources;
using Identity.Domain.RoleAggregate.Entities;
using Identity.Domain.RoleAggregate.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Identity.Application.UseCases.Roles.Commands;

public class DeleteRoleCommandHandler : ICommandHandler<DeleteRoleCommand>
{
    private readonly RoleManager<ApplicationRole> _roleManager;

    public DeleteRoleCommandHandler(RoleManager<ApplicationRole> roleManager)
    {
        _roleManager = roleManager;
    }
    
    public async Task Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleManager.FindByIdAsync(request.Id);
        if (role == null)
        {
            throw new RoleNotFoundException(request.Id);
        }
        
        role.Destroy();

        var result = await _roleManager.DeleteAsync(role);
        if (!result.Succeeded)
        {
            var error = result.Errors.First();
            throw new ResourceInvalidOperationException(error.Description, error.Code);
        }
    }
}