using Application.Dtos.Forms;
using BuildingBlocks.Application.Cqrs;
using Submission.Application.UseCases.Dtos;
using Submission.Domain.FormAggregate.Repositories;

namespace Submission.Application.UseCases.Forms.Queries;

public class GetSpacesFormQueryHandler : IQueryHandler<GetSpacesFormQuery, List<BasicFormDto>>
{
    private readonly IFormRepository _formRepository;
    
    public GetSpacesFormQueryHandler(IFormRepository formRepository)
    {
        _formRepository = formRepository;
    }
    
    public Task<List<BasicFormDto>> Handle(GetSpacesFormQuery request, CancellationToken cancellationToken)
    {
        return _formRepository.GetBySpacesAsync(request.SpaceIds, new FormQueryDto());
    }
}