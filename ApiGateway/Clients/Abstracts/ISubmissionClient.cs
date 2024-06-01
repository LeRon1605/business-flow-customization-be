using Application.Dtos.Forms;
using Application.Dtos.Submissions.Responses;
using BuildingBlocks.Application.Clients;

namespace ApiGateway.Clients.Abstracts;

public interface ISubmissionClient : IRestSharpClient
{
    Task<List<BasicFormDto>> GetFormsBySpacesAsync(List<int> spaceIds);

    Task<List<BasicSubmissionDto>> GetSubmissionDataAsync(List<int> submissionIds);
}