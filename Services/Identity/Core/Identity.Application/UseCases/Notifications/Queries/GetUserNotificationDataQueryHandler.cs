using Application.Dtos.Notifications.Responses;
using BuildingBlocks.Application.Cqrs;
using Identity.Application.UseCases.Notifications.Dtos;
using Identity.Domain.UserAggregate;

namespace Identity.Application.UseCases.Notifications.Queries;

public class GetUserNotificationDataQueryHandler : IQueryHandler<GetUserNotificationDataQuery, IList<IdentityNotificationDataDto>>
{
    private readonly IUserRepository _userRepository;
    
    public GetUserNotificationDataQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<IList<IdentityNotificationDataDto>> Handle(GetUserNotificationDataQuery request, CancellationToken cancellationToken)
    {
        return await _userRepository.FindByIncludedIdsAsync(request.UserIds, new IdentityNotificationDataQueryDto());
    }
}