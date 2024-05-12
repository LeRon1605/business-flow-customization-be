using BuildingBlocks.Domain.Exceptions.Resources;
using Domain;
using Submission.Domain.FormAggregate.Entities;

namespace Submission.Domain.FormAggregate.Exceptions;

public class SpaceFormAlreadyExistedException : ResourceAlreadyExistException
{
    public SpaceFormAlreadyExistedException(int spaceId) 
        : base(nameof(Form), nameof(Form.SpaceId), spaceId.ToString(), ErrorCodes.SpaceFormAlreadyExisted)
    {
    }
}