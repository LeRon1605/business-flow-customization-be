namespace BusinessFlow.Domain.BusinessFlowAggregate.Models;

public class BusinessFlowOutComeModel<TKey> where TKey : IEquatable<TKey>
{
    public TKey? Id { get; set; }
    
    public string Name { get; set; } = null!;

    public string Color { get; set; } = null!;
}