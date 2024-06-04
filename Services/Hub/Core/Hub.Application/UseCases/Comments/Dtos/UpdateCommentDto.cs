using Hub.Domain.CommentAggregate.Entities;

namespace Hub.Application.UseCases.Comments.Dtos;

public class UpdateCommentDto
{
    public string Content { get; set; } = null!;
    
    public List<CommentMention> Mentions { get; set; } = new();
}