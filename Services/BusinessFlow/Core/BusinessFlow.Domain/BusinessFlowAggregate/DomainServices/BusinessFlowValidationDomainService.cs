using BusinessFlow.Domain.BusinessFlowAggregate.DomainServices.Interfaces;
using BusinessFlow.Domain.BusinessFlowAggregate.Enums;
using BusinessFlow.Domain.BusinessFlowAggregate.Models;

namespace BusinessFlow.Domain.BusinessFlowAggregate.DomainServices;

public class BusinessFlowValidationDomainService : IBusinessFlowValidationDomainService
{
    public List<BusinessFlowBlockValidationModel<TKey>> Validate<TKey>(List<BusinessFlowBlockModel<TKey>> blocks
        , List<BusinessFlowBranchModel<TKey>> branches) where TKey : IEquatable<TKey>
    {
        var result = new List<BusinessFlowBlockValidationModel<TKey>>();
        
        result.AddRange(ValidateStartBlocks(blocks, branches));
        result.AddRange(ValidateFunctionBlocks(blocks, branches));
        result.AddRange(ValidateEndBlocks(blocks, branches));

        return result;
    }

    private List<BusinessFlowBlockValidationModel<TKey>> ValidateStartBlocks<TKey>(List<BusinessFlowBlockModel<TKey>> blocks
        , List<BusinessFlowBranchModel<TKey>> branches) where TKey : IEquatable<TKey>
    {
        var result = new List<BusinessFlowBlockValidationModel<TKey>>();
        
        var startBlocks = blocks
            .Where(x => x.Type == BusinessFlowBlockType.Start)
            .ToList();
        if (!startBlocks.Any())
            return result;

        var isExceedOneStartBlock = startBlocks.Count > 1;
        foreach (var startBlock in startBlocks)
        {
            var validation = new BusinessFlowBlockValidationModel<TKey>()
            {
                Id = startBlock.Id,
                ErrorMessages = new List<string>()
            };
            
            var fromBlockBranches = branches
                .Where(x => Equals(x.FromBlockId, startBlock.Id))
                .ToList();
            if (!fromBlockBranches.Any())
                validation.ErrorMessages.Add("Khối bắt đầu phải có một nhánh đi.");
            if (fromBlockBranches.Count > 1)
                validation.ErrorMessages.Add("Khối bắt đầu chỉ được phép có một nhánh đi.");
            
            var toBlockBranches = branches
                .Where(x => Equals(x.ToBlockId, startBlock.Id))
                .ToList();
            if (toBlockBranches.Any())
                validation.ErrorMessages.Add("Khối bắt đầu không được có nhánh đến.");
            
            if (isExceedOneStartBlock)
                validation.ErrorMessages.Add("Quy trình nghiệp vụ chỉ được phép có một khối bắt đầu.");
            
            result.Add(validation);
        }
        
        return result;
    }

    private List<BusinessFlowBlockValidationModel<TKey>> ValidateEndBlocks<TKey>(List<BusinessFlowBlockModel<TKey>> blocks
        , List<BusinessFlowBranchModel<TKey>> branches) where TKey : IEquatable<TKey>
    {
        var result = new List<BusinessFlowBlockValidationModel<TKey>>();
        
        var endBlocks = blocks
            .Where(x => x.Type == BusinessFlowBlockType.End)
            .ToList();
        if (!endBlocks.Any())
            return result;
        
        foreach (var endBlock in endBlocks)
        {
            var validation = new BusinessFlowBlockValidationModel<TKey>()
            {
                Id = endBlock.Id,
                ErrorMessages = new List<string>()
            };
            
            var toBlockBranches = branches
                .Where(x => Equals(x.ToBlockId, endBlock.Id))
                .ToList();
            if (!toBlockBranches.Any())
                validation.ErrorMessages.Add("Khối kết thúc phải có ít nhất một nhánh đến.");

            var fromBlockBranches = branches
                .Where(x => Equals(x.FromBlockId, endBlock.Id))
                .ToList();
            if (fromBlockBranches.Any())
                validation.ErrorMessages.Add("Khối kết thúc không được có nhánh đi.");
            
            result.Add(validation);
        }
        
        return result;
    }
    
    private List<BusinessFlowBlockValidationModel<TKey>> ValidateFunctionBlocks<TKey>(List<BusinessFlowBlockModel<TKey>> blocks
        , List<BusinessFlowBranchModel<TKey>> branches) where TKey : IEquatable<TKey>
    {
        var result = new List<BusinessFlowBlockValidationModel<TKey>>();
        
        var functionBlocks = blocks
            .Where(x => x.Type == BusinessFlowBlockType.Function)
            .ToList();
        if (!functionBlocks.Any())
            return result;
        
        foreach (var functionBlock in functionBlocks)
        {
            var validation = new BusinessFlowBlockValidationModel<TKey>()
            {
                Id = functionBlock.Id,
                ErrorMessages = new List<string>()
            };
            
            var isHasOutCome = functionBlock.OutComes.Any();
            if (!isHasOutCome)
                validation.ErrorMessages.Add("Khối chức năng phải có ít nhất một kết quả.");
            
            var isOutComeDuplicate = functionBlock.OutComes
                .GroupBy(x => x.Name)
                .Any(x => x.Count() > 1);
            if (isOutComeDuplicate)
                validation.ErrorMessages.Add("Khối chức năng không thể có kết quả trùng nhau.");
            
            var isHasToBranch = branches
                .Any(x => Equals(x.ToBlockId, functionBlock.Id));
            if (!isHasToBranch)
                validation.ErrorMessages.Add("Hãy liên kết khối chức năng với các khối khác.");
            
            var fromBlockBranches = branches
                .Where(x => Equals(x.FromBlockId, functionBlock.Id))
                .ToList();
            if (fromBlockBranches.Count < functionBlock.OutComes.Count)
                validation.ErrorMessages.Add("Hãy thêm nghiệp vụ tương ứng với kết quả đầu ra.");
            
            result.Add(validation);
        }
        
        return result;
    }
}