using BuildingBlocks.Domain.Services;
using BusinessFlow.Domain.BusinessFlowAggregate.Models;

namespace BusinessFlow.Domain.BusinessFlowAggregate.DomainServices.Interfaces;

public interface IBusinessFlowValidationDomainService : IDomainService
{
    List<BusinessFlowBlockValidationModel<TKey>> Validate<TKey>(List<BusinessFlowBlockModel<TKey>> blocks
        , List<BusinessFlowBranchModel<TKey>> branches) where TKey : IEquatable<TKey>;
}