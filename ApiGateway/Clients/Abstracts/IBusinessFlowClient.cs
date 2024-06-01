using Application.Dtos.Spaces;
using BuildingBlocks.Application.Clients;

namespace ApiGateway.Clients.Abstracts;

public interface IBusinessFlowClient : IRestSharpClient
{
    Task<List<SpaceDto>> GetSpacesAsync();
}