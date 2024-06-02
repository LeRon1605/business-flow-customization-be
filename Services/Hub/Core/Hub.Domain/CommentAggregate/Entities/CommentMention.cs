using Hub.Domain.CommentAggregate.Enums;

namespace Hub.Domain.CommentAggregate.Entities;

public class CommentMention
{
    public MentionEntity EntityType { get; private set; }
    
    public List<string> EntityIds { get; private set; }
    
    public CommentMention(MentionEntity entityType, List<string> entityIds)
    {
        EntityType = entityType;
        EntityIds = entityIds;
    }
}