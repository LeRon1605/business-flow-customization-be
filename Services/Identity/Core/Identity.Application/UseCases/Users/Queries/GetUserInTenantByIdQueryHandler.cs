using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Domain.Repositories;
using Identity.Application.UseCases.Users.Dtos;
using Identity.Domain.TenantAggregate.Entities;
using Identity.Domain.TenantAggregate.Exceptions;
using Identity.Domain.UserAggregate.Entities;
using Identity.Domain.UserAggregate.Exceptions;

namespace Identity.Application.UseCases.Users.Queries;

public class GetUserInTenantByIdQueryHandler: IQueryHandler<GetUserInTenantByIdQuery, UserDetailDto>
{
    private readonly IBasicReadOnlyRepository<Tenant, int> _tenantRepository;
    private readonly IBasicReadOnlyRepository<ApplicationUser, string> _userRepository;
    
    public GetUserInTenantByIdQueryHandler(IBasicReadOnlyRepository<Tenant, int> tenantRepository
        , IBasicReadOnlyRepository<ApplicationUser, string> userRepository)
    {
        _tenantRepository = tenantRepository;
        _userRepository = userRepository;
    }
    
    public async Task<UserDetailDto> Handle(GetUserInTenantByIdQuery request, CancellationToken cancellationToken)
    {
        var tenant = await _tenantRepository.FindByIdAsync(request.TenantId);
        if (tenant == null)
        {
            throw new TenantNotFoundException(request.TenantId);
        }
        
        var user = await _userRepository.FindByIdAsync(request.UserId, new UserDetailDto());
        if (user == null)
        {
            throw new UserNotFoundException(request.UserId);
        }

        return user;
    }
}