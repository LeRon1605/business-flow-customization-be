﻿using System.Linq.Expressions;
using BuildingBlocks.Domain.Repositories;
using Identity.Domain.UserAggregate.Entities;
using ApplicationUser = Identity.Domain.UserAggregate.Entities.ApplicationUser;

namespace Identity.Application.UseCases.Users.Dtos;

public class UserBasicInfoDto: IProjection<ApplicationUser, string, UserBasicInfoDto>
{
    public string Id { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string AvatarUrl { get; set; } = null!;
    
    public Expression<Func<ApplicationUser, UserBasicInfoDto>> GetProject()
    {
        return _ => new UserBasicInfoDto
        {
            Id = _.Id,
            UserName = _.UserName,
            Email = _.Email,
            AvatarUrl = _.AvatarUrl,
        };
    }
}