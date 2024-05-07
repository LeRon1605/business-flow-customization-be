using BuildingBlocks.Domain.Exceptions.Resources;
using BusinessFlow.Domain.BusinessFlowAggregate.Entities;
using Domain;

namespace BusinessFlow.Domain.BusinessFlowAggregate.Exceptions;

public class BusinessFlowNotFoundException : ResourceNotFoundException
{
    public BusinessFlowNotFoundException(int id) 
        : base(nameof(BusinessFlowVersion), nameof(BusinessFlowVersion.Id), id.ToString(), ErrorCodes.BusinessFlowNotFound)
    {
    }
}