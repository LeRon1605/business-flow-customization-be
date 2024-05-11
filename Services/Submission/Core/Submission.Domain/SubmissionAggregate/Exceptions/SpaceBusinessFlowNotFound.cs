using BuildingBlocks.Domain.Exceptions.Resources;
using Domain;
using Submission.Domain.SpaceBusinessFlowAggregate.Entities;

namespace Submission.Domain.SubmissionAggregate.Exceptions;

public class SpaceBusinessFlowNotFound : ResourceNotFoundException
{
    public SpaceBusinessFlowNotFound(int spaceId) 
        : base(nameof(SpaceBusinessFlow), nameof(SpaceBusinessFlow.SpaceId), spaceId.ToString(), ErrorCodes.BusinessFlowNotFound)
    {
    }
}