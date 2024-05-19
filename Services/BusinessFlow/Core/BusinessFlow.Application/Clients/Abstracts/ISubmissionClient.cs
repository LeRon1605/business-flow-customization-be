using Application.Dtos.Submissions.Requests;
using Application.Dtos.Submissions.Responses;
using BuildingBlocks.Application.Clients;
using BuildingBlocks.Application.Dtos;

namespace BusinessFlow.Application.Clients.Abstracts;

public interface ISubmissionClient : IRestSharpClient
{
    Task<SimpleIdResponse<int>> CreateFormAsync(int spaceId, FormRequestDto formDto);
    
    Task<BusinessFlowBlocksElementsResponse> GetBusinessFlowBlocksElementsAsync(int spaceId, List<Guid> businessFlowBlockIds);
}