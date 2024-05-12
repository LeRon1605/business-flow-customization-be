using BuildingBlocks.Domain.Exceptions.Resources;
using Domain;
using Submission.Domain.FormAggregate.Entities;

namespace Submission.Domain.FormAggregate.Exceptions;

public class SpaceFormNotFoundException : ResourceNotFoundException
{
    public SpaceFormNotFoundException(int spaceId)
        : base(nameof(Form), nameof(Form.SpaceId), spaceId.ToString(), ErrorCodes.FormNotFound)
    {
    }
}