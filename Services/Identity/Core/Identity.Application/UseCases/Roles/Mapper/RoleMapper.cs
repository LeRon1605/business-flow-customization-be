using BuildingBlocks.Application.Mappers;
using Identity.Application.UseCases.Roles.Dtos;
using Identity.Domain.RoleAggregate.Entities;

namespace Identity.Application.UseCases.Roles.Mapper;

public class RoleMapper : MappingProfile
{
    public RoleMapper()
    {
        CreateMap<ApplicationRole, RoleDto>();
    }
}