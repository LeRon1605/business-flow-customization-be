﻿using Application.Dtos;
using Application.Dtos.Identity;
using AutoMapper;
using BuildingBlocks.Application.Cqrs;
using Identity.Domain.PermissionAggregate;

namespace Identity.Application.UseCases.Permissions.Queries;

public class GetAllPermissionQueryHandler : IQueryHandler<GetAllPermissionQuery, IEnumerable<PermissionDto>>
{
    private readonly IPermissionRepository _permissionRepository;
    private readonly IMapper _mapper;

    public GetAllPermissionQueryHandler(IPermissionRepository permissionRepository
        , IMapper mapper)
    {
        _permissionRepository = permissionRepository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<PermissionDto>> Handle(GetAllPermissionQuery request, CancellationToken cancellationToken)
    {
        var permissions = await _permissionRepository.FilterAsync(request.Name);
        return _mapper.Map<IEnumerable<PermissionDto>>(permissions);
    }
}