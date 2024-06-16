using BuildingBlocks.Domain.Models;
using BusinessFlow.Domain.BusinessFlowAggregate.DomainEvents;
using BusinessFlow.Domain.BusinessFlowAggregate.Enums;
using BusinessFlow.Domain.BusinessFlowAggregate.Exceptions;
using BusinessFlow.Domain.BusinessFlowAggregate.Models;
using BusinessFlow.Domain.SpaceAggregate.Entities;

namespace BusinessFlow.Domain.BusinessFlowAggregate.Entities;

public class BusinessFlowVersion : FullAuditableTenantAggregateRoot
{
    public int SpaceId { get; private set; }

    public BusinessFlowStatus Status { get; private set; }

    public virtual Space Space { get; private set; } = null!;

    public virtual List<BusinessFlowBlock> Blocks { get; private set; } = new();

    public BusinessFlowVersion(List<BusinessFlowBlockModel> blocks, List<BusinessFlowBranchModel> branches)
    {
        Status = BusinessFlowStatus.Published;
        AddBlocks(blocks);
        AddBranches(branches);
        
        AddDomainEvent(new BusinessFlowVersionCreatedDomainEvent(this));
    }

    public void AddBlocks(List<BusinessFlowBlockModel> blocks)
    {
        foreach (var block in blocks)
        {
            Blocks.Add(new BusinessFlowBlock(block));
        }
    }

    public void AddBranches(List<BusinessFlowBranchModel> branches)
    {
        foreach (var branch in branches)
        {
            var fromBlock = Blocks.FirstOrDefault(b => b.Id == branch.FromBlockId);
            var toBlock = Blocks.FirstOrDefault(b => b.Id == branch.ToBlockId);
            if (fromBlock == null || toBlock == null)
            {
                throw new InvalidBusinessFlowBranchException();
            }

            var branchEntity = new BusinessFlowBranch(branch);
            fromBlock.AddBranch(branchEntity);
            toBlock.AddBranch(branchEntity);
        }
    }
    
    private BusinessFlowVersion()
    {
        
    }
}