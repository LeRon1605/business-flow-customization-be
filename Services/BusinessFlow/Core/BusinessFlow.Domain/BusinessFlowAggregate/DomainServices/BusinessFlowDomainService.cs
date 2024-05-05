using BusinessFlow.Domain.BusinessFlowAggregate.DomainServices.Interfaces;
using BusinessFlow.Domain.BusinessFlowAggregate.Entities;
using BusinessFlow.Domain.BusinessFlowAggregate.Exceptions;
using BusinessFlow.Domain.BusinessFlowAggregate.Models;
using BusinessFlow.Domain.BusinessFlowAggregate.Repositories;
using BusinessFlow.Domain.SpaceAggregate.Entities;
using BusinessFlow.Domain.SpaceAggregate.Exceptions;

namespace BusinessFlow.Domain.BusinessFlowAggregate.DomainServices;

public class BusinessFlowDomainService : IBusinessFlowDomainService
{
    private readonly IBusinessFlowVersionRepository _businessFlowVersionRepository;
    private readonly IBusinessFlowValidationDomainService _businessFlowValidationDomainService;
    
    public BusinessFlowDomainService(IBusinessFlowVersionRepository businessFlowVersionRepository
        , IBusinessFlowValidationDomainService businessFlowValidationDomainService)
    {
        _businessFlowVersionRepository = businessFlowVersionRepository;
        _businessFlowValidationDomainService = businessFlowValidationDomainService;
    }
    
    public async Task CreateAsync(Space space, List<BusinessFlowBlockModel> blocks, List<BusinessFlowBranchModel> branches)
    {
        if (space.BusinessFlowVersions.Any())
        {
            throw new SpaceBusinessFlowAlreadyCreatedException(space.Id);
        }
        
        var isValid = ValidateBusinessFlow(blocks, branches);
        if (!isValid)
        {
            throw new InvalidBusinessFlowException();
        }
        
        var businessFlowVersion = new BusinessFlowVersion(blocks, branches);
        space.AddBusinessFlowVersion(businessFlowVersion);
        
        await _businessFlowVersionRepository.InsertAsync(businessFlowVersion);
    }
    
    private bool ValidateBusinessFlow(List<BusinessFlowBlockModel> blocks, List<BusinessFlowBranchModel> branches)
    {
        var validation = _businessFlowValidationDomainService.Validate(blocks, branches);
        return validation.All(x => !x.ErrorMessages.Any());
    }
}