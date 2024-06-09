using System.Linq.Expressions;
using BuildingBlocks.Domain.Repositories;
using ApplicationUser = Identity.Domain.UserAggregate.Entities.ApplicationUser;

namespace Identity.Application.UseCases.Users.Dtos;

public class UserDto: IProjection<ApplicationUser, string, UserDto>
{
    public string Id { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string AvatarUrl { get; set; } = null!;
    public string Role { get; set; } = null!;
    public int CurrentTenantId { get; set; }
    
    public Expression<Func<ApplicationUser, UserDto>> GetProject()
    {
        return _ => new UserDto
        {
            Id = _.Id,
            FullName = _.FullName,
            UserName = _.UserName,
            Email = _.Email,
            AvatarUrl = _.AvatarUrl,
            Role = _.Roles.FirstOrDefault(x => x.TenantId == CurrentTenantId).Role.Name ?? string.Empty
        };
    }
}