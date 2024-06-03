using System.Linq.Expressions;
using Application.Dtos.Notifications.Responses;
using BuildingBlocks.Domain.Repositories;
using Identity.Domain.UserAggregate.Entities;

namespace Identity.Application.UseCases.Notifications.Dtos;

public class IdentityNotificationDataQueryDto : IdentityNotificationDataDto, IProjection<ApplicationUser, string, IdentityNotificationDataDto>
{
    public Expression<Func<ApplicationUser, IdentityNotificationDataDto>> GetProject()
    {
        return user => new IdentityNotificationDataDto
        {
            Id = user.Id,
            FullName = user.FullName
        };
    }
}