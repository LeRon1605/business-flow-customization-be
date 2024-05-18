namespace Application.Dtos.Submissions.Requests;

public class BusinessFlowBlocksElementsRequest
{
    public List<Guid> BusinessFlowBlockIds { get; set; } = new();
}