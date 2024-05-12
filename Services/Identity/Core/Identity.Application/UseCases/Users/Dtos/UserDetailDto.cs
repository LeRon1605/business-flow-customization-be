using System.Linq.Expressions;
using Application.Dtos;
using Application.Dtos.Submissions.Identity;
using BuildingBlocks.Domain.Repositories;
using ApplicationUser = Identity.Domain.UserAggregate.Entities.ApplicationUser;

namespace Identity.Application.UseCases.Users.Dtos;

public class UserDetailDto: IProjection<ApplicationUser, string, UserDetailDto>
{
    public string Id { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string AvatarUrl { get; set; } = null!;
    public IEnumerable<TenantDto> Tenants { get; set; } = null!;
    
    public Expression<Func<ApplicationUser, UserDetailDto>> GetProject()
    {
        return _ => new UserDetailDto()
        {
            Id = _.Id,
            UserName = _.UserName,
            Email = _.Email,
            PhoneNumber = _.PhoneNumber,
            AvatarUrl = _.AvatarUrl,
        };
    }
}