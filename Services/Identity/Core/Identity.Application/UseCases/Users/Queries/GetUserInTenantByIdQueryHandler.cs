using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Identity;
using BuildingBlocks.Domain.Repositories;
using Identity.Application.UseCases.Users.Dtos;
using Identity.Domain.TenantAggregate.Entities;
using Identity.Domain.TenantAggregate.Exceptions;
using Identity.Domain.UserAggregate.Entities;
using Identity.Domain.UserAggregate.Exceptions;

namespace Identity.Application.UseCases.Users.Queries;

public class GetUserInTenantByIdQueryHandler: IQueryHandler<GetUserInTenantByIdQuery, UserDetailDto>
{
    private readonly ICurrentUser _currentUser;
    private readonly IBasicReadOnlyRepository<Tenant, int> _tenantRepository;
    private readonly IBasicReadOnlyRepository<ApplicationUser, string> _userRepository;
    
    public GetUserInTenantByIdQueryHandler(
        ICurrentUser currentUser,
        IBasicReadOnlyRepository<Tenant, int> tenantRepository,
        IBasicReadOnlyRepository<ApplicationUser, string> userRepository)
    {
        _currentUser = currentUser;
        _tenantRepository = tenantRepository;
        _userRepository = userRepository;
    }
    
    public async Task<UserDetailDto> Handle(GetUserInTenantByIdQuery request, CancellationToken cancellationToken)
    {
        var tenant = await _tenantRepository.FindByIdAsync(_currentUser.TenantId);
        if (tenant == null)
        {
            throw new TenantNotFoundException(_currentUser.TenantId);
        }
        
        var user = await _userRepository.FindByIdAsync(request.Id, new UserDetailDto());
        if (user == null)
        {
            throw new UserNotFoundException(request.Id);
        }

        return user;
    }
}