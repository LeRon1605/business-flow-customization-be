using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Dtos;
using Submission.Application.UseCases.Submissions.Dtos;
using Submission.Domain.SubmissionAggregate.Entities;

namespace Submission.Application.UseCases.Submissions.Queries;

public class GetSubmissionQuery : PagingAndSortingRequestDto, IQuery<PagedResultDto<SubmissionDto>>
{
    public int SpaceId { get; set; }
    
    public int FormVersionId { get; set; }
    
    public GetSubmissionQuery(int spaceId, int formVersionId, int page, int size) : base(page, size, $"{nameof(FormSubmission.Created)} DESC")
    {
        SpaceId = spaceId;
        FormVersionId = formVersionId;
    }
}