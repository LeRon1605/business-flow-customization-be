using Application.Dtos.Forms;
using BuildingBlocks.Application.Cqrs;

namespace Submission.Application.UseCases.Forms.Queries;

public class GetSpacesFormQuery : IQuery<List<BasicFormDto>>
{
    public List<int> SpaceIds { get; set; }
    
    public GetSpacesFormQuery(List<int> spaceIds)
    {
        SpaceIds = spaceIds;
    }
}