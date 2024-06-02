using BuildingBlocks.Domain.Events;
using Hub.Domain.CommentAggregate.Entities;

namespace Hub.Domain.CommentAggregate.DomainEvents;

public class CommentCreatedDomainEvent : IDomainEvent
{
    public Comment Comment { get; set; }
    
    public CommentCreatedDomainEvent(Comment comment)
    {
        Comment = comment;
    }
}