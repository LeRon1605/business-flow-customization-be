using BuildingBlocks.Domain.Exceptions.Resources;
using Domain;
using Submission.Domain.FormAggregate.Entities;

namespace Submission.Domain.FormAggregate.Exceptions;

public class BusinessFlowBlockFormNotFoundException : ResourceNotFoundException
{
    public BusinessFlowBlockFormNotFoundException(Guid businessFlowBlockId) 
        : base(nameof(Form), nameof(Form.BusinessFlowBlockId), businessFlowBlockId.ToString(), ErrorCodes.BusinessFlowBlockFormNotFound)
    {
    }
}