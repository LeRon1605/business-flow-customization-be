using BuildingBlocks.Domain.Services;
using BusinessFlow.Domain.BusinessFlowAggregate.Entities;
using BusinessFlow.Domain.SpaceAggregate.Entities;

namespace BusinessFlow.Domain.BusinessFlowAggregate.DomainServices.Interfaces;

public interface IBusinessFlowDomainService : IDomainService
{
    Task<BusinessFlowVersion> CreateAsync(Space space, BusinessFlowModel businessFlow);
}