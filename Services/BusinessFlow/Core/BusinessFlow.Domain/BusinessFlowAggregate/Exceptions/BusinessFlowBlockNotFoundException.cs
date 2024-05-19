using BuildingBlocks.Domain.Exceptions.Resources;
using BusinessFlow.Domain.BusinessFlowAggregate.Entities;
using Domain;

namespace BusinessFlow.Domain.BusinessFlowAggregate.Exceptions;

public class BusinessFlowBlockNotFoundException : ResourceNotFoundException
{
    public BusinessFlowBlockNotFoundException(Guid id) 
        : base(nameof(BusinessFlowBlock), nameof(BusinessFlowBlock.Id), id.ToString(), ErrorCodes.BusinessFlowBlockNotFound)
    {
    }
}