namespace Cdc.BussinessFlows;

public class SpaceModel
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    
    public string? Description { get; set; }
    
    public string? Color { get; set; }
    
    public int TenantId { get; set; }
    
    public string? CreatedBy { get; set; }
}