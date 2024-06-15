using BuildingBlocks.Application.Cqrs;
using Submission.Application.UseCases.Dtos;
using Submission.Domain.FormAggregate.Exceptions;
using Submission.Domain.FormAggregate.Repositories;

namespace Submission.Application.UseCases.Forms.Queries;

public class GetPublicFormQueryHandler : IQueryHandler<GetPublicFormQuery, FormDto>
{
    private readonly IFormRepository _formRepository;
    private readonly IFormVersionRepository _formVersionRepository;
    
    public GetPublicFormQueryHandler(IFormRepository formRepository, IFormVersionRepository formVersionRepository)
    {
        _formRepository = formRepository;
        _formVersionRepository = formVersionRepository;
    }
    
    public async Task<FormDto> Handle(GetPublicFormQuery request, CancellationToken cancellationToken)
    {
        var form = await _formRepository.FindByPublicTokenAsync(request.Token);
        if (form == null)
        {
            throw new FormNotFoundException();
        }
        
        var targetForm = await _formVersionRepository.GetLatestSpaceVersionAsync(form.SpaceId, new FormDto());
        if (targetForm == null)
        {
            throw new SpaceFormNotFoundException(form.SpaceId);
        }
        
        return targetForm;
    }
}

