using BuildingBlocks.Application.Cqrs;

namespace BusinessFlow.Application.UseCases.Spaces.Commands;

public class DeleteMemberInSpaceCommand : ICommand
{
    public int SpaceId { get; set; }
    public string UserId { get; set; }
    public DeleteMemberInSpaceCommand(int spaceId, string userId)
    {
        SpaceId = spaceId;
        UserId = userId;
    }
}