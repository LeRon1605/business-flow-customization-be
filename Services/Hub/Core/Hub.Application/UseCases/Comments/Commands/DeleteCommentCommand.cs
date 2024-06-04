using BuildingBlocks.Application.Cqrs;

namespace Hub.Application.UseCases.Comments.Commands;

public class DeleteCommentCommand : ICommand
{
    public Guid Id { get; set; }
    
    public DeleteCommentCommand(Guid id)
    {
        Id = id;
    }
}