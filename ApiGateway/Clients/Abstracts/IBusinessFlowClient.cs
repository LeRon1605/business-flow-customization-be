using Application.Dtos.Spaces;
using Application.Dtos.SubmissionExecutions;
using BuildingBlocks.Application.Clients;

namespace ApiGateway.Clients.Abstracts;

public interface IBusinessFlowClient : IRestSharpClient
{
    Task<List<SpaceDto>> GetSpacesAsync();

    Task<List<AssignedSubmissionExecutionDto>> GetAssignedSubmissionExecutionsAsync();
}