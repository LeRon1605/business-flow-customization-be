using BuildingBlocks.Application.Cqrs;

namespace BusinessFlow.Application.UseCases.Spaces.Commands;

public class UpdateRoleSpaceMemberCommand : ICommand
{
    public int SpaceId { get; set; }
    public string UserId { get; set; }
    public int RoleId { get; set; }
    
    public UpdateRoleSpaceMemberCommand(int spaceId, string userId, int roleId)
    {
        SpaceId = spaceId;
        UserId = userId;
        RoleId = roleId;
    }
}