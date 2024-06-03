using Application.Dtos.Notifications.Responses;
using BuildingBlocks.Application.Clients;

namespace Hub.Application.Clients.Abstracts;

public interface IInternalIdentityClient : IRestSharpClient
{
    Task<List<IdentityNotificationDataDto>> GetIdentityNotificationDataAsync(List<string> userIds);
}