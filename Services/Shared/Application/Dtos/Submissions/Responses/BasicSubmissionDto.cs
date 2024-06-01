namespace Application.Dtos.Submissions.Responses;

public class BasicSubmissionDto 
{
    public int Id { get; set; }
    
    public string Name { get; set; } = null!;
    
    public int FormVersionId { get; set; }
}