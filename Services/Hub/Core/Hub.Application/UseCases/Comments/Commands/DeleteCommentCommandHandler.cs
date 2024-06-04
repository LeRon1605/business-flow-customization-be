using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Identity;
using Hub.Domain.CommentAggregate.DomainServices;

namespace Hub.Application.UseCases.Comments.Commands;

public class DeleteCommentCommandHandler : ICommandHandler<DeleteCommentCommand>
{
    private readonly ICurrentUser _currentUser;
    private readonly ICommentDomainService _commentDomainService;
    
    public DeleteCommentCommandHandler(ICurrentUser currentUser, ICommentDomainService commentDomainService)
    {
        _currentUser = currentUser;
        _commentDomainService = commentDomainService;
    }
    
    public async Task Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        await _commentDomainService.DeleteAsync(request.Id, _currentUser.Id);
    }
}