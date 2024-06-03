using Application.Dtos.Notifications.Responses;
using BuildingBlocks.Application.Clients;

namespace Hub.Application.Clients.Abstracts;

public interface IInternalSubmissionClient : IRestSharpClient
{
    Task<List<SubmissionNotificationDataDto>> GetSubmissionNotificationDataAsync(List<int> submissionIds);
}