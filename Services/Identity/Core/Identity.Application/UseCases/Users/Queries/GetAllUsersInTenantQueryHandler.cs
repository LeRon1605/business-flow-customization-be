﻿using AutoMapper;
using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Dtos;
using BuildingBlocks.Domain.Repositories;
using BuildingBlocks.Domain.Specifications.Interfaces;
using Identity.Application.UseCases.Users.Dtos;
using Identity.Domain.TenantAggregate.Entities;
using Identity.Domain.TenantAggregate.Exceptions;
using Identity.Domain.UserAggregate.Specifications;
using ApplicationUser = Identity.Domain.UserAggregate.Entities.ApplicationUser;

namespace Identity.Application.UseCases.Users.Queries;

public class GetAllUsersInTenantQueryHandler : IQueryHandler<GetAllUsersInTenantQuery, PagedResultDto<UserBasicInfoDto>>
{
    private readonly IBasicReadOnlyRepository<Tenant, int> _tenantRepository;
    private readonly IBasicReadOnlyRepository<ApplicationUser, string> _userRepository;
    
    public GetAllUsersInTenantQueryHandler(
        IBasicReadOnlyRepository<Tenant, int> tenantRepository,
        IBasicReadOnlyRepository<ApplicationUser, string> userRepository)
    {
        _tenantRepository = tenantRepository;
        _userRepository = userRepository;
    }
    
    public async Task<PagedResultDto<UserBasicInfoDto>> Handle(GetAllUsersInTenantQuery request, CancellationToken cancellationToken)
    {
        var tenant = await _tenantRepository.FindByIdAsync(request.Id);
        if (tenant == null)
        {
            throw new TenantNotFoundException(request.Id);
        }
        
        var specification = GetSpecification(request);
        var users = await _userRepository.GetPagedListAsync(specification, new UserBasicInfoDto());
        var total = await _userRepository.GetCountAsync(specification);
        
        return new PagedResultDto<UserBasicInfoDto>(
            total,
            request.Size,
            users
        );
    }
    
    private IPagingAndSortingSpecification<ApplicationUser, string> GetSpecification(GetAllUsersInTenantQuery request)
    {
        var specification =
            new FilterApplicationUserSpecification(request.Page, request.Size, request.Sorting, request.Search)
                .And(new UserByTenantSpecification(request.Id));

        return specification;
    }
}