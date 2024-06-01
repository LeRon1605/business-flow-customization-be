namespace Application.Dtos.SubmissionExecutions;

public class AssignedSubmissionExecutionDto
{
    public int Id { get; set; }
    
    public Guid BusinessFlowId { get; set; }
    
    public string BusinessFlowName { get; set; } = null!;
    
    public int SpaceId { get; set; }
    
    public string SpaceName { get; set; } = null!;
    
    public string SpaceColor { get; set; } = null!;
    
    public int SubmissionId { get; set; }
}