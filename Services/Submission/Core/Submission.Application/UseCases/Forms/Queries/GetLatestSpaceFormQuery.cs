using BuildingBlocks.Application.Cqrs;
using Submission.Application.UseCases.Dtos;

namespace Submission.Application.UseCases.Forms.Queries;

public class GetLatestSpaceFormQuery : IQuery<FormDto>
{
    public int SpaceId { get; set; }
    
    public GetLatestSpaceFormQuery(int spaceId)
    {
        SpaceId = spaceId;
    }
}