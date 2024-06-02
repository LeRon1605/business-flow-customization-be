using Hub.Domain.CommentAggregate.Entities;

namespace Hub.Application.UseCases.Comments.Dtos;

public class CommentDto
{
    public Guid Id { get; set; }
    
    public string Content { get; set; } = null!;
    
    public Guid? ParentId { get; set; }
    
    public List<CommentMention> Mentions { get; set; } = new();
    
    public DateTime Created { get; set; }
    
    public string? CreatedBy { get; set; }
    
    public DateTime? LastModified { get; set; }
    
    public string? LastModifiedBy { get; set; }
}