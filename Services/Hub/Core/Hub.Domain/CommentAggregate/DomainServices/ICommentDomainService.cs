using BuildingBlocks.Domain.Services;
using Hub.Domain.CommentAggregate.Entities;
using Hub.Domain.CommentAggregate.Enums;

namespace Hub.Domain.CommentAggregate.DomainServices;

public interface ICommentDomainService : IDomainService
{
    Task<Comment> CreateAsync(string content
        , string entityId
        , CommentEntity entityType
        , Guid? parentId
        , List<CommentMention> mentions);
}