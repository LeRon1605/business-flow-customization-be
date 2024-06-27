using BuildingBlocks.Application.Cqrs;

namespace Submission.Application.UseCases.Forms.Commands;

public class PublishFormCommand : ICommand
{
    public int SpaceId { get; set; }
    public bool IsPublished { get; set; }
    public PublishFormCommand(int spaceId, bool isPublished)
    {
        SpaceId = spaceId;
        IsPublished = isPublished;
    }
}