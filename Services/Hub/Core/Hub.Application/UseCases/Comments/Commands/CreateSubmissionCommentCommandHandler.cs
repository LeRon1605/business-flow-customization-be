using BuildingBlocks.Application.Cqrs;
using Hub.Domain.CommentAggregate.DomainServices;
using Hub.Domain.CommentAggregate.Enums;

namespace Hub.Application.UseCases.Comments.Commands;

public class CreateSubmissionCommentCommandHandler : ICommandHandler<CreateSubmissionCommentCommand, Guid>
{
    private readonly ICommentDomainService _commentDomainService;
    
    public CreateSubmissionCommentCommandHandler(ICommentDomainService commentDomainService)
    {
        _commentDomainService = commentDomainService;
    }
    
    public async Task<Guid> Handle(CreateSubmissionCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _commentDomainService.CreateAsync(request.Comment.Content
            , request.SubmissionId.ToString()
            , CommentEntity.Submission
            , request.Comment.ParentId
            , request.Comment.Mentions);
        
        return comment.Id;
    }
}