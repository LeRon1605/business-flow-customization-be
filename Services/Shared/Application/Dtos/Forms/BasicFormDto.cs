namespace Application.Dtos.Forms;

public class BasicFormDto
{
    public int Id { get; set; }
    
    public string Name { get; set; } = null!;
    
    public int SpaceId { get; set; }
    
    public int VersionId { get; set; }
}