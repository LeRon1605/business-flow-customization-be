using Application.Dtos.Forms;
using BuildingBlocks.Application.Clients;

namespace ApiGateway.Clients.Abstracts;

public interface ISubmissionClient : IRestSharpClient
{
    Task<List<BasicFormDto>> GetFormsBySpacesAsync(List<int> spaceIds);
}