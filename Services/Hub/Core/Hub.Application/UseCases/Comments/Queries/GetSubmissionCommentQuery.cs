using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Dtos;
using Hub.Application.UseCases.Comments.Dtos;

namespace Hub.Application.UseCases.Comments.Queries;

public class GetSubmissionCommentQuery : PagingRequestDto, IQuery<PagedResultDto<CommentDto>>
{
    public int SubmissionId { get; set; }
    
    public GetSubmissionCommentQuery(int submissionId, int page, int size)
    {
        SubmissionId = submissionId;
        Page = page;
        Size = size;
    }
}