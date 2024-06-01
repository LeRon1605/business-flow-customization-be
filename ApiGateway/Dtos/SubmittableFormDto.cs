using Application.Dtos.Forms;

namespace ApiGateway.Dtos;

public class SubmittableFormDto : BasicFormDto
{
    public string SpaceName { get; set; } = null!;
    
    public string SpaceColor { get; set; } = null!;
}