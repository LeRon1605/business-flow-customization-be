namespace BusinessFlow.Domain.BusinessFlowAggregate.Models;

public class BusinessFlowModel
{
    public List<BusinessFlowBlockModel> Blocks { get; set; }
    
    public List<BusinessFlowBranchModel> Branches { get; set; }
    
    private BusinessFlowModel(List<BusinessFlowBlockModel> blocks, List<BusinessFlowBranchModel> branches)
    {
        Blocks = blocks;
        Branches = branches;
    }
    
    public static BusinessFlowBlockModelFactoryResult Create(List<BusinessFlowBlockModel> blocks, List<BusinessFlowBranchModel> branches)
    {
        var mappingGeneratedIds = GenerateIdentifier(blocks, branches);
        
        return new BusinessFlowBlockModelFactoryResult()
        {
            BusinessFlowModel = new BusinessFlowModel(blocks, branches),
            MappingGeneratedIds = mappingGeneratedIds
        };
    }

    private static Dictionary<Guid, Guid> GenerateIdentifier(List<BusinessFlowBlockModel> blocks, List<BusinessFlowBranchModel> branches)
    {
        var mappingGeneratedIds = new Dictionary<Guid, Guid>();
        
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
            
            mappingGeneratedIds.Add(block.Id, generatedBlockId);
            block.Id = generatedBlockId;
        }

        return mappingGeneratedIds;
    }
}

public class BusinessFlowBlockModelFactoryResult
{
    public BusinessFlowModel BusinessFlowModel { get; set; } = null!;

    public Dictionary<Guid, Guid> MappingGeneratedIds { get; set; } = null!;
}