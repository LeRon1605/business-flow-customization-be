namespace BusinessFlow.Domain.BusinessFlowAggregate.Models;

public class BusinessFlowOutComeModel
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = null!;

    public string Color { get; set; } = null!;
}