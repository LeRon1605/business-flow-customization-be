using Application.Dtos;
using Application.Dtos.Identity;
using BuildingBlocks.Application.Mappers;
using Identity.Domain.PermissionAggregate.Entities;

namespace Identity.Application.UseCases.Permissions.Mapper;

public class PermissionMapper : MappingProfile
{
    public PermissionMapper()
    {
        CreateMap<ApplicationPermission, PermissionDto>();
    }
}