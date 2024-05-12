using BuildingBlocks.Application.Cqrs;
using Submission.Application.UseCases.Dtos;

namespace Submission.Application.UseCases.Forms.Queries;

public class GetFormVersionQuery : IQuery<FormDto>
{
    public int SpaceId { get; set; }
    
    public int VersionId { get; set; }
    
    public GetFormVersionQuery(int spaceId, int versionId)
    {
        SpaceId = spaceId;
        VersionId = versionId;
    }
}