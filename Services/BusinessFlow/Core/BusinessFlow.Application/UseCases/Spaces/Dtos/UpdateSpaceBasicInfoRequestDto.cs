using System.ComponentModel.DataAnnotations;

namespace BusinessFlow.Application.UseCases.Spaces.Dtos;

public class UpdateSpaceBasicInfoRequestDto
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Color { get; set; } = null!;
    
    public UpdateSpaceBasicInfoRequestDto(string name, string description, string color)
    {
        Name = name;
        Description = description;
        Color = color;
    }
}