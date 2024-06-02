using Hub.Domain.CommentAggregate.Entities;
using Hub.Domain.CommentAggregate.Enums;
using Hub.Domain.CommentAggregate.Repositories;

namespace Hub.Domain.CommentAggregate.DomainServices;

public class CommentDomainService : ICommentDomainService
{
    private readonly ICommentRepository _commentRepository;
    
    public CommentDomainService(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }
    
    public async Task<Comment> CreateAsync(string content
        , string entityId
        , CommentEntity entityType
        , Guid? parentId
        , List<CommentMention> mentions)
    {
        var comment = new Comment(content, entityId, entityType, parentId, mentions);

        await _commentRepository.InsertAsync(comment);
        
        return comment;
    }
}