using Hub.Domain.CommentAggregate.Entities;

namespace Hub.Application.UseCases.Comments.Dtos;

public class CreateSubmissionCommentDto
{
    public string Content { get; set; } = null!;
    
    public Guid? ParentId { get; set; }

    public List<CommentMention> Mentions { get; set; } = new();
}