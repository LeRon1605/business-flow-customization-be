using BuildingBlocks.Domain.Exceptions.Resources;
using Domain;
using Submission.Domain.FormAggregate.Entities;

namespace Submission.Domain.FormAggregate.Exceptions;

public class FormVersionNotFoundException : ResourceNotFoundException
{
    public FormVersionNotFoundException(int spaceId, int versionId) 
        : base($"Form version {versionId} in space {spaceId} does not exist", ErrorCodes.FormVersionNotFound)
    {
    }
}