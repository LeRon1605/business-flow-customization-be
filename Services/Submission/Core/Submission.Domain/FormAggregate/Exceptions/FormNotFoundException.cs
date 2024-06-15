using BuildingBlocks.Domain.Exceptions.Resources;
using Domain;

namespace Submission.Domain.FormAggregate.Exceptions;

public class FormNotFoundException : ResourceNotFoundException
{
    public FormNotFoundException() 
        : base($"Form does not exist", ErrorCodes.FormNotFound)
    {
    }
}