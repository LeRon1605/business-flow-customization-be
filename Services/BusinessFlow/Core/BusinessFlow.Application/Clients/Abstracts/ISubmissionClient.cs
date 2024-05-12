using Application.Dtos.Submissions.Requests;
using BuildingBlocks.Application.Clients;

namespace BusinessFlow.Application.Clients.Abstracts;

public interface ISubmissionClient : IRestSharpClient
{
    Task CreateFormAsync(int spaceId, FormRequestDto formDto);
}