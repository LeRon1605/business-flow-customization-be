using BuildingBlocks.Domain.Services;
using BusinessFlow.Domain.BusinessFlowAggregate.Models;

namespace BusinessFlow.Domain.BusinessFlowAggregate.DomainServices.Interfaces;

public interface IBusinessFlowValidationDomainService : IDomainService
{
    List<BusinessFlowBlockValidationModel> Validate(List<BusinessFlowBlockModel> blocks
        , List<BusinessFlowBranchModel> branches);
}