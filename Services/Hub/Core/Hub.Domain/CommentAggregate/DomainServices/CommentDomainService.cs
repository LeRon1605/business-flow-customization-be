using Hub.Domain.CommentAggregate.Entities;
using Hub.Domain.CommentAggregate.Enums;
using Hub.Domain.CommentAggregate.Exceptions;
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
    
    public async Task UpdateAsync(Guid commentId, string content, List<CommentMention> mentions, string senderId)
    {
        var comment = await _commentRepository.FindAsync(commentId, senderId);
        if (comment == null)
        {
            throw new CommentNotFoundException(commentId);
        }
        
        comment.UpdateContent(content);
        comment.UpdateMentions(mentions);
        
        _commentRepository.Update(comment);
    }

    public async Task DeleteAsync(Guid commentId, string senderId)
    {
        var comment = await _commentRepository.FindAsync(commentId, senderId);
        if (comment == null)
        {
            throw new CommentNotFoundException(commentId);
        }
        
        _commentRepository.Delete(comment);
    }
}