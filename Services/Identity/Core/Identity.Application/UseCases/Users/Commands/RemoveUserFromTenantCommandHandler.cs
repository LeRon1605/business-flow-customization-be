﻿using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Identity;
using Identity.Domain.TenantAggregate;
using Identity.Domain.TenantAggregate.Exceptions;
using Identity.Domain.UserAggregate;
using Identity.Domain.UserAggregate.Exceptions;
using Microsoft.Extensions.Logging;

namespace Identity.Application.UseCases.Users.Commands;

public class RemoveUserFromTenantCommandHandler : ICommandHandler<RemoveUserFromTenantCommand>
{
    private ICurrentUser _currentUser;
    private readonly ITenantRepository _tennantRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RemoveUserFromTenantCommandHandler> _logger;
    
    public RemoveUserFromTenantCommandHandler(
        ICurrentUser currentUser,
        ITenantRepository tennantRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        ILogger<RemoveUserFromTenantCommandHandler> logger)
    {
        _currentUser = currentUser;
        _tennantRepository = tennantRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    
    public async Task Handle(RemoveUserFromTenantCommand request, CancellationToken cancellationToken)
    {
        var tenant = await _tennantRepository.FindByIdAsync(_currentUser.TenantId);
        if (tenant == null)
        {
            throw new TenantNotFoundException(_currentUser.TenantId);
        }
        
        var user = await _userRepository.FindByIdAsync(request.UserId);
        if (user == null)
        {
            throw new UserNotFoundException(request.UserId);
        }
        user.RemoveFromTenant(_currentUser.TenantId);
    }
}