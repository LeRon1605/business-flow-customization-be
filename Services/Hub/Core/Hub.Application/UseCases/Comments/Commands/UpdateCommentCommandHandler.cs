using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Identity;
using Hub.Domain.CommentAggregate.DomainServices;

namespace Hub.Application.UseCases.Comments.Commands;

public class UpdateCommentCommandHandler : ICommandHandler<UpdateCommentCommand>
{
    private readonly ICurrentUser _currentUser;
    private readonly ICommentDomainService _commentDomainService;
    
    public UpdateCommentCommandHandler(ICurrentUser currentUser, ICommentDomainService commentDomainService)
    {
        _currentUser = currentUser;
        _commentDomainService = commentDomainService;
    }
    
    public Task Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
    {
        return _commentDomainService.UpdateAsync(request.Id, request.Comment.Content, request.Comment.Mentions,  _currentUser.Id);
    }
}