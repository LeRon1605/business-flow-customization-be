using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Dtos;
using BuildingBlocks.Domain.Repositories;
using BuildingBlocks.Domain.Specifications.Interfaces;
using BuildingBlocks.Kernel.Models;
using Domain.Enums;
using Newtonsoft.Json;
using Submission.Application.UseCases.Submissions.Dtos;
using Submission.Application.UseCases.Submissions.Enums;
using Submission.Domain.FormAggregate.Entities;
using Submission.Domain.SubmissionAggregate.Entities;
using Submission.Domain.SubmissionAggregate.Specifications;

namespace Submission.Application.UseCases.Submissions.Queries;

public class GetSubmissionQueryHandler : IQueryHandler<GetSubmissionQuery, PagedResultDto<SubmissionDto>>
{
    private readonly IBasicReadOnlyRepository<FormSubmission, int> _submissionRepository;
    private readonly IBasicReadOnlyRepository<FormElement, int> _elementRepository;
    
    public GetSubmissionQueryHandler(IBasicReadOnlyRepository<FormSubmission, int> submissionRepository
        , IBasicReadOnlyRepository<FormElement, int> elementRepository)
    {
        _submissionRepository = submissionRepository;
        _elementRepository = elementRepository;
    }
    
    public async Task<PagedResultDto<SubmissionDto>> Handle(GetSubmissionQuery request, CancellationToken cancellationToken)
    {
        var specification = await GetSpecificationAsync(request);
        
        var submissions = await _submissionRepository.GetPagedListAsync(specification, new SubmissionDto());
        var total = await _submissionRepository.GetCountAsync(specification);
        
        return new PagedResultDto<SubmissionDto>(total, request.Size, submissions);
    }
    
    private async Task<IPagingAndSortingSpecification<FormSubmission, int>> GetSpecificationAsync(GetSubmissionQuery request)
    {
        var specification = new SubmissionBySpacePagingSpecification(request.Page, request.Size, request.Sorting, request.SpaceId, request.Search)
            .And(new SubmissionByVersionSpecification(request.FormVersionId));

        if (request.FilterFields?.Any() is true)
        {
            var elementFilterFields = request.FilterFields
                .Where(x => x.Type == SubmissionFilterFieldType.RecordElement)
                .Select(x => JsonConvert.DeserializeObject<SubmissionRecordElementFilterFieldDto>(x.Value))
                .ToList();
            var elementIds = elementFilterFields.Select(x => x.ElementId).ToList();
            
            var elements = await _elementRepository.FindByIncludedIdsAsync(elementIds);
            foreach (var element in elements)
            {
                var filterField = elementFilterFields.FirstOrDefault(x => x.ElementId == element.Id);
                if (filterField is null)
                    continue;

                switch (element.Type)
                {
                    case FormElementType.Text:
                        var textValue = filterField.Value;
                        specification = specification.And(new SubmissionTextFieldSpecification(element.Id, textValue));
                        break;
                    
                    case FormElementType.SingleOption:
                    case FormElementType.MultiOption:
                        var optionIds = JsonConvert.DeserializeObject<int[]>(filterField.Value);
                        if (optionIds is null)
                            continue;
                        
                        specification = specification.And(new SubmissionOptionFieldSpecification(element.Id, optionIds));
                        break;
                    
                    case FormElementType.Date:
                        var dateRanges = JsonConvert.DeserializeObject<List<TimeRange>>(filterField.Value);
                        if (dateRanges is null)
                            continue;
                        
                        specification = specification.And(new SubmissionDateFieldSpecification(element.Id, dateRanges));
                        break;
                }
            }
        }
        
        return specification;
    }
}