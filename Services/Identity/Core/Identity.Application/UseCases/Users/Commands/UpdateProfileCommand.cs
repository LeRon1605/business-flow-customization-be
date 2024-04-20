using BuildingBlocks.Application.Cqrs;

namespace Identity.Application.UseCases.Users.Commands;

public class UpdateProfileCommand : ICommand
{
    public string FullName { get; set; }
    public string AvatarUrl { get; set; }
    
    public UpdateProfileCommand(string fullName, string avatarUrl)
    {
        FullName = fullName;
        AvatarUrl = avatarUrl;
    }
}