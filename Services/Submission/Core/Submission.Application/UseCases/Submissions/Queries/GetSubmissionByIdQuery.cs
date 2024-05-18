using BuildingBlocks.Application.Cqrs;
using Submission.Application.UseCases.Submissions.Dtos;

namespace Submission.Application.UseCases.Submissions.Queries;

public class GetSubmissionByIdQuery : IQuery<SubmissionDto>
{
    public int Id { get; set; }

    public int SpaceId { get; set; }

    public int FormVersionId { get; set; }

    public GetSubmissionByIdQuery(int id, int spaceId, int formVersionId)
    {
        Id = id;
        SpaceId = spaceId;
        FormVersionId = formVersionId;
    }
}