﻿using AutoMapper;
using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Dtos;
using BuildingBlocks.Application.Identity;
using BuildingBlocks.Domain.Repositories;
using BuildingBlocks.Domain.Specifications.Interfaces;
using Identity.Application.UseCases.Roles.Queries;
using Identity.Application.UseCases.Users.Dtos;
using Identity.Domain.RoleAggregate;
using Identity.Domain.TenantAggregate.Entities;
using Identity.Domain.TenantAggregate.Exceptions;
using Identity.Domain.UserAggregate;
using Identity.Domain.UserAggregate.Specifications;
using ApplicationUser = Identity.Domain.UserAggregate.Entities.ApplicationUser;

namespace Identity.Application.UseCases.Users.Queries;

public class GetAllUsersInTenantQueryHandler : IQueryHandler<GetAllUsersInTenantQuery, PagedResultDto<UserDto>>
{
    private readonly ICurrentUser _currentUser;
    private readonly IBasicReadOnlyRepository<Tenant, int> _tenantRepository;
    private readonly IBasicReadOnlyRepository<ApplicationUser, string> _userRepository;
    
    public GetAllUsersInTenantQueryHandler(
        ICurrentUser currentUser,
        IBasicReadOnlyRepository<Tenant, int> tenantRepository,
        IBasicReadOnlyRepository<ApplicationUser, string> userRepository,
        IRoleRepository roleRepository)
    {
        _currentUser = currentUser;
        _tenantRepository = tenantRepository;
        _userRepository = userRepository;
    }
    
    public async Task<PagedResultDto<UserDto>> Handle(GetAllUsersInTenantQuery request, CancellationToken cancellationToken)
    {
        var tenant = await _tenantRepository.FindByIdAsync(_currentUser.TenantId);
        if (tenant == null)
        {
            throw new TenantNotFoundException(_currentUser.TenantId);
        }
        
        var specification = GetSpecification(request);
        var users = await _userRepository.GetPagedListAsync(specification, new UserDto(){CurrentTenantId = _currentUser.TenantId});
        users.OrderBy(x => x.UserName);
        var total = await _userRepository.GetCountAsync(specification);
        
        return new PagedResultDto<UserDto>(
            total,
            request.Size,
            users
        );
    }
    
    private IPagingAndSortingSpecification<ApplicationUser, string> GetSpecification(GetAllUsersInTenantQuery request)
    {
        var specification =
            new FilterApplicationUserSpecification(request.Page, request.Size, request.Sorting, request.Search)
                .And(new UserByTenantSpecification(_currentUser.TenantId));

        return specification;
    }
}