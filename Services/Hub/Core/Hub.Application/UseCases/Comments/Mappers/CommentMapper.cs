using BuildingBlocks.Application.Mappers;
using Hub.Application.UseCases.Comments.Dtos;
using Hub.Domain.CommentAggregate.Entities;

namespace Hub.Application.UseCases.Comments.Mappers;

public class CommentMapper : MappingProfile
{
    public CommentMapper()
    {
        CreateMap<Comment, CommentDto>();
    }
}