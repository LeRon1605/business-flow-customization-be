using Application.Dtos.Identity;
using BuildingBlocks.Application.Identity.Dtos;
using BuildingBlocks.Application.Mappers;
using Identity.Application.UseCases.Users.Dtos;
using Identity.Domain.UserAggregate.Entities;

namespace Identity.Application.UseCases.Users.Mapper;

public class UserMapper : MappingProfile
{
    public UserMapper()
    {
        CreateMap<ApplicationUser, IdentityUserDto>();
        CreateMap<ApplicationUser, UserBasicInfoDto>();
        CreateMap<ApplicationUser, UserDetailDto>();
        CreateMap<UserBasicInfoDto, BasicUserInfoDto>();
    }
}