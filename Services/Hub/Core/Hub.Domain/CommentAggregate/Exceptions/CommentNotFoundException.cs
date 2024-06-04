using BuildingBlocks.Domain.Exceptions.Resources;
using Domain;
using Hub.Domain.CommentAggregate.Entities;

namespace Hub.Domain.CommentAggregate.Exceptions;

public class CommentNotFoundException : ResourceNotFoundException
{
    public CommentNotFoundException(Guid id) : base(nameof(Comment), nameof(Comment.Id), id.ToString(), ErrorCodes.CommentNotFound)
    {
    }
}