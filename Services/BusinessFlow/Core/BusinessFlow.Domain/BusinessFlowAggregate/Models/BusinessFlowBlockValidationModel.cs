namespace BusinessFlow.Domain.BusinessFlowAggregate.Models;

public class BusinessFlowBlockValidationModel
{
    public Guid Id { get; set; }

    public List<string> ErrorMessages { get; set; } = new();
}