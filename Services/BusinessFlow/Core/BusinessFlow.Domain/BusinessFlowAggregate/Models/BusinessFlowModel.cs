namespace BusinessFlow.Domain.BusinessFlowAggregate.Models;

public class BusinessFlowModel
{
    public List<BusinessFlowBlockModel> Blocks { get; set; }
    
    public List<BusinessFlowBranchModel> Branches { get; set; }
    
    public BusinessFlowModel(List<BusinessFlowBlockModel> blocks, List<BusinessFlowBranchModel> branches)
    {
        GenerateIdentifier(blocks, branches);
        
        Blocks = blocks;
        Branches = branches;
    }

    private void GenerateIdentifier(List<BusinessFlowBlockModel> blocks, List<BusinessFlowBranchModel> branches)
    {
        foreach (var block in blocks)
        {
            var generatedBlockId = Guid.NewGuid();

            var affectedBranches = branches
                .Where(b => b.FromBlockId == block.Id || b.ToBlockId == block.Id)
                .ToList();
            foreach (var branch in affectedBranches)
            {
                if (branch.FromBlockId == block.Id)
                {
                    branch.FromBlockId = generatedBlockId;
                }
                if (branch.ToBlockId == block.Id)
                {
                    branch.ToBlockId = generatedBlockId;
                }
            }
            
            foreach (var outcome in block.OutComes)
            {
                var generatedOutcomeId = Guid.NewGuid();
                
                var affectedOutComeBranches = branches
                    .Where(b => b.OutComeId == outcome.Id)
                    .ToList();
                foreach (var branch in affectedOutComeBranches)
                {
                    branch.OutComeId = generatedOutcomeId;
                }
                
                outcome.Id = generatedOutcomeId;
            }
            
            block.Id = generatedBlockId;
        }
    }
}