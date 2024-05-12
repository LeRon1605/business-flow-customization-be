using BuildingBlocks.Application.Cqrs;
using Submission.Application.UseCases.Dtos;
using Submission.Domain.FormAggregate.Exceptions;
using Submission.Domain.FormAggregate.Repositories;

namespace Submission.Application.UseCases.Forms.Queries;

public class GetFormVersionQueryHandler : IQueryHandler<GetFormVersionQuery, FormDto>
{
    private readonly IFormVersionRepository _formVersionRepository;
    
    public GetFormVersionQueryHandler(IFormVersionRepository formVersionRepository)
    {
        _formVersionRepository = formVersionRepository;
    }
    
    public async Task<FormDto> Handle(GetFormVersionQuery request, CancellationToken cancellationToken)
    {
        var form = await _formVersionRepository.GetAsync(request.SpaceId, request.VersionId, new FormDto());
        if (form == null)
        {
            throw new FormVersionNotFoundException(request.SpaceId, request.VersionId);
        }
        
        return form;
    }
}