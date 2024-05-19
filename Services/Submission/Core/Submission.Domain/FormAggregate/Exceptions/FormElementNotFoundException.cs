using BuildingBlocks.Domain.Exceptions.Resources;
using Domain;

namespace Submission.Domain.FormAggregate.Exceptions;

public class FormElementNotFoundException : ResourceNotFoundException
{
    public FormElementNotFoundException(int elementId) 
        : base($"Element {elementId} not found", ErrorCodes.FormElementNotFound)
    {
    }
    
    public FormElementNotFoundException(int elementId, int versionId, int spaceId) 
        : base($"Element {elementId} not found in version {versionId} and space {spaceId}", ErrorCodes.FormElementNotFound)
    {
    }
}