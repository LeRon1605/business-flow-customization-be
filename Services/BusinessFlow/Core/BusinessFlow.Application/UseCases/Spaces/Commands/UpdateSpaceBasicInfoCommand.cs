using BuildingBlocks.Application.Cqrs;

namespace BusinessFlow.Application.UseCases.Spaces.Commands;

public class UpdateSpaceBasicInfoCommand : ICommand
{
    public int Id { get; set; } 
    public string Name { get; set; } 
    public string Description { get; set; }
    public string Color { get; set; } 
    
    public UpdateSpaceBasicInfoCommand(int id, string name, string description, string color)
    {
        Id = id;
        Name = name;
        Description = description;
        Color = color;
    }
}