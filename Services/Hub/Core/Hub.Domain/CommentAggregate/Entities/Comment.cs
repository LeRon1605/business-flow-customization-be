using BuildingBlocks.Domain.Models;
using Hub.Domain.CommentAggregate.DomainEvents;
using Hub.Domain.CommentAggregate.Enums;

namespace Hub.Domain.CommentAggregate.Entities;

public class Comment : AuditableTenantAggregateRoot<Guid>
{
    public string Content { get; private set; }
    
    public string EntityId { get; private set; }
    
    public Guid? ParentId { get; private set; }
    
    public CommentEntity EntityType { get; private set; }

    public List<CommentMention> Mentions { get; private set; } = new();
    
    public Comment(string content, string entityId, CommentEntity entityType, Guid? parentId, List<CommentMention> mentions)
    {
        Content = content;
        EntityId = entityId;
        ParentId = parentId;
        EntityType = entityType;
        UpdateMentions(mentions);
        
        AddDomainEvent(new CommentCreatedDomainEvent(this));
    }
    
    public void UpdateContent(string content)
    {
        Content = content;
    }
    
    public void UpdateMentions(List<CommentMention> mentions)
    {
        foreach (var mention in mentions)
        {
            var existingMention = Mentions.FirstOrDefault(x => x.EntityType == mention.EntityType);
            
            if (existingMention != null)
            {
                existingMention.EntityIds.Clear();
                existingMention.EntityIds.AddRange(mention.EntityIds);
            }
            else
            {
                Mentions.Add(mention);
            }
        }
    }
}