using BuildingBlocks.Application.Cqrs;
using Hub.Application.UseCases.Comments.Dtos;
using Hub.Domain.CommentAggregate.Entities;

namespace Hub.Application.UseCases.Comments.Commands;

public class UpdateCommentCommand : ICommand
{
    public Guid Id { get; set; }
    
    public UpdateCommentDto Comment { get; set; }
    
    public UpdateCommentCommand(Guid id, UpdateCommentDto comment)
    {
        Id = id;
        Comment = comment;
    }
}