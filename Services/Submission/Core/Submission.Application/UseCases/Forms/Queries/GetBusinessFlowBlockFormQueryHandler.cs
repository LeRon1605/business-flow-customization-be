using BuildingBlocks.Application.Cqrs;
using Submission.Application.UseCases.Dtos;
using Submission.Domain.FormAggregate.Exceptions;
using Submission.Domain.FormAggregate.Repositories;

namespace Submission.Application.UseCases.Forms.Queries;

public class GetBusinessFlowBlockFormQueryHandler : IQueryHandler<GetBusinessFlowBlockFormQuery, FormDto>
{
    private readonly IFormVersionRepository _formVersionRepository;
    
    public GetBusinessFlowBlockFormQueryHandler(IFormVersionRepository formVersionRepository)
    {
        _formVersionRepository = formVersionRepository;
    }
    
    public async Task<FormDto> Handle(GetBusinessFlowBlockFormQuery request, CancellationToken cancellationToken)
    {
        var form = await _formVersionRepository.GetAsync(request.BusinessFlowBlockId, new FormDto());
        if (form == null)
        {
            throw new BusinessFlowBlockFormNotFoundException(request.BusinessFlowBlockId);
        }
        
        return form;
    }
}