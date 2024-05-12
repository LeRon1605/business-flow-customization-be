using BuildingBlocks.Application.Cqrs;
using Submission.Application.UseCases.Dtos;
using Submission.Domain.FormAggregate.Repositories;

namespace Submission.Application.UseCases.Forms.Queries;

public class GetSpaceFormVersionQueryHandler : IQueryHandler<GetSpaceFormVersionQuery, List<FormVersionDto>>
{
    private readonly IFormVersionRepository _formVersionRepository;

    public GetSpaceFormVersionQueryHandler(IFormVersionRepository formVersionRepository)
    {
        _formVersionRepository = formVersionRepository;
    }
    
    public async Task<List<FormVersionDto>> Handle(GetSpaceFormVersionQuery request, CancellationToken cancellationToken)
    {
        return await _formVersionRepository.GetBySpaceAsync(request.SpaceId, new FormVersionDto());
    }
}