using BuildingBlocks.Application.Cqrs;
using Submission.Application.UseCases.Dtos;
using Submission.Domain.FormAggregate.Exceptions;
using Submission.Domain.FormAggregate.Repositories;

namespace Submission.Application.UseCases.Forms.Queries;

public class GetLatestSpaceQueryHandler : IQueryHandler<GetLatestSpaceFormQuery, FormDto>
{
    private readonly IFormVersionRepository _formVersionRepository;
    
    public GetLatestSpaceQueryHandler(IFormVersionRepository formVersionRepository)
    {
        _formVersionRepository = formVersionRepository;
    }
    
    public async Task<FormDto> Handle(GetLatestSpaceFormQuery request, CancellationToken cancellationToken)
    {
        var form = await _formVersionRepository.GetLatestSpaceVersionAsync(request.SpaceId, new FormDto());
        if (form == null)
        {
            throw new SpaceFormNotFoundException(request.SpaceId);
        }
        
        return form;
    }
}