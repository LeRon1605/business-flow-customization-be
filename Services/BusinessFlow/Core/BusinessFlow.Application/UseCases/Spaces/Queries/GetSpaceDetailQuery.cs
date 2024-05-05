using BuildingBlocks.Application.Cqrs;
using BusinessFlow.Application.UseCases.Spaces.Dtos;

namespace BusinessFlow.Application.UseCases.Spaces.Queries;

public class GetSpaceDetailQuery : IQuery<SpaceDetailDto>
{
    public int Id { get; set; }
    
    public GetSpaceDetailQuery(int id)
    {
        Id = id;
    }
}