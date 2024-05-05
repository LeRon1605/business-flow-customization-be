namespace BusinessFlow.Domain.BusinessFlowAggregate.Models;

public class BusinessFlowBranchModel
{
    public Guid FromBlockId { get; set; }
    
    public Guid ToBlockId { get; set; }
    
    public Guid? OutComeId { get; set; }
}