using BuildingBlocks.Application.Dtos;

namespace Submission.Application.UseCases.Submissions.Dtos;

public class SubmissionFilterRequestDto : PagingRequestDto
{
    public int FormVersionId { get; set; }
}