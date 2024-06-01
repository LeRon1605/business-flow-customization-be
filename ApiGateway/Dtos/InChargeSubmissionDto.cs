namespace ApiGateway.Dtos;

public class InChargeSubmissionDto
{
    public int Id { get; set; }
    
    public string Name { get; set; } = null!;
    
    public int FormVersionId { get; set; }
    
    public Guid BusinessFlowId { get; set; }
    
    public string BusinessFlowName { get; set; } = null!;
    
    public int SpaceId { get; set; }
    
    public string SpaceName { get; set; } = null!;
    
    public string SpaceColor { get; set; } = null!;
}