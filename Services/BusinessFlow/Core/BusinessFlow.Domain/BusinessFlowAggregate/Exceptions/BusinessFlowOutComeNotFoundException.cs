using BuildingBlocks.Domain.Exceptions.Resources;
using Domain;

namespace BusinessFlow.Domain.BusinessFlowAggregate.Exceptions;

public class BusinessFlowOutComeNotFoundException : ResourceNotFoundException
{
    public BusinessFlowOutComeNotFoundException(Guid blockId, Guid outComeId) 
        : base($"Outcome {outComeId} not found in block {blockId}", ErrorCodes.BusinessFlowOutComeNotFound)
    {
    }
}