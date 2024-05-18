namespace Application.Dtos.Submissions.Responses;

public class BusinessFlowBlocksElementsResponse
{
    public Dictionary<Guid, List<FormElementDto>> Elements { get; set; } = new();
}