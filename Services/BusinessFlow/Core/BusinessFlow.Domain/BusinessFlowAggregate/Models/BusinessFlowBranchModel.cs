namespace BusinessFlow.Domain.BusinessFlowAggregate.Models;

public class BusinessFlowBranchModel<TKey> where TKey : IEquatable<TKey>
{
    public TKey? FromBlockId { get; set; }
    
    public TKey? ToBlockId { get; set; }
    
    public TKey? OutComeId { get; set; }
}