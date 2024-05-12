using BuildingBlocks.Application.Cqrs;
using Submission.Application.UseCases.Dtos;

namespace Submission.Application.UseCases.Forms.Queries;

public class GetSpaceFormVersionQuery : IQuery<List<FormVersionDto>>
{
    public int SpaceId { get; set; }

    public GetSpaceFormVersionQuery(int spaceId)
    {
        SpaceId = spaceId;
    }
}