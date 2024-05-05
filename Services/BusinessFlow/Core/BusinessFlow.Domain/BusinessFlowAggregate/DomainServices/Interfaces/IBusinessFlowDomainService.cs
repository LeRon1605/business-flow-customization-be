using BuildingBlocks.Domain.Services;
using BusinessFlow.Domain.BusinessFlowAggregate.Models;
using BusinessFlow.Domain.SpaceAggregate.Entities;

namespace BusinessFlow.Domain.BusinessFlowAggregate.DomainServices.Interfaces;

public interface IBusinessFlowDomainService : IDomainService
{
    Task CreateAsync(Space space, List<BusinessFlowBlockModel> blocks, List<BusinessFlowBranchModel> branches);
}