using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Domain.Repositories;
using BusinessFlow.Application.UseCases.Spaces.Dtos;
using BusinessFlow.Domain.SpaceAggregate.Entities;
using BusinessFlow.Domain.SpaceAggregate.Exceptions;

namespace BusinessFlow.Application.UseCases.Spaces.Queries;

public class GetSpaceDetailQueryHandler : IQueryHandler<GetSpaceDetailQuery, SpaceDetailDto>
{
    private readonly IBasicReadOnlyRepository<Space, int> _spaceRepository;
    
    public GetSpaceDetailQueryHandler(IBasicReadOnlyRepository<Space, int> spaceRepository)
    {
        _spaceRepository = spaceRepository;
    }
    
    public async Task<SpaceDetailDto> Handle(GetSpaceDetailQuery request, CancellationToken cancellationToken)
    {
        var space = await _spaceRepository.FindByIdAsync(request.Id, new SpaceDetailDto());
        if (space == null)
        {
            throw new SpaceNotFoundException(request.Id);
        }
        
        return space;
    }
}