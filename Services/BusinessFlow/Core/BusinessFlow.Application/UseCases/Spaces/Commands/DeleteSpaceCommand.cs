using BuildingBlocks.Application.Cqrs;

namespace BusinessFlow.Application.UseCases.Spaces.Commands;

public class DeleteSpaceCommand: ICommand
{
    public int SpaceId { get; set; }
    public DeleteSpaceCommand(int spaceId)
    {
        SpaceId = spaceId;
    }
}
