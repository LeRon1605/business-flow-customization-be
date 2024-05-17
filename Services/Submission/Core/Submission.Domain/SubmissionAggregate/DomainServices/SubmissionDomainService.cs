using BuildingBlocks.Domain.Repositories;
using Domain.Enums;
using Submission.Domain.FormAggregate.Entities;
using Submission.Domain.FormAggregate.Exceptions;
using Submission.Domain.FormAggregate.Specifications;
using Submission.Domain.SpaceBusinessFlowAggregate.Repositories;
using Submission.Domain.SubmissionAggregate.DomainServices.Abstracts;
using Submission.Domain.SubmissionAggregate.Entities;
using Submission.Domain.SubmissionAggregate.Exceptions;
using Submission.Domain.SubmissionAggregate.Models;
using Submission.Domain.SubmissionAggregate.Repositories;

namespace Submission.Domain.SubmissionAggregate.DomainServices;

public class SubmissionDomainService : ISubmissionDomainService
{
    private readonly IFormSubmissionRepository _submissionRepository;
    private readonly ISpaceBusinessFlowRepository _spaceBusinessFlowRepository;
    private readonly ISubmissionFieldCreatorFactory _submissionFieldCreatorFactory;
    private readonly IBasicReadOnlyRepository<FormElement, int> _formElementRepository;

    public SubmissionDomainService(IFormSubmissionRepository submissionRepository
        , ISpaceBusinessFlowRepository spaceBusinessFlowRepository
        , ISubmissionFieldCreatorFactory submissionFieldCreatorFactory
        , IBasicReadOnlyRepository<FormElement, int> formElementRepository)
    {
        _submissionRepository = submissionRepository;
        _spaceBusinessFlowRepository = spaceBusinessFlowRepository;
        _submissionFieldCreatorFactory = submissionFieldCreatorFactory;
        _formElementRepository = formElementRepository;
    }
    
    public async Task<FormSubmission> CreateAsync(int spaceId, SubmissionModel submissionModel)
    {
        var businessFlow = await _spaceBusinessFlowRepository.FindBySpaceId(spaceId);
        if (businessFlow == null)
        {
            throw new SpaceBusinessFlowNotFound(spaceId);
        }

        var submission = new FormSubmission(submissionModel.Name
            , submissionModel.FormVersionId
            , businessFlow.BusinessFlowVersionId);
        
        var elementIds = submissionModel.Fields.Select(x => x.ElementId).ToList();
        var formElements = await GetElementsAsync(spaceId, submissionModel.FormVersionId, elementIds);

        foreach (var field in submissionModel.Fields)
        {
            var formElement = formElements.First(x => x.Id == field.ElementId);
            
            var creator = _submissionFieldCreatorFactory.Get(formElement.Type);
            var submissionField = creator.Create(formElement, field);

            submission.AddField(submissionField);
        }

        await _submissionRepository.InsertAsync(submission);

        return submission;
    }
    
    private async Task<List<FormElement>> GetElementsAsync(int spaceId, int versionId, List<int> elementIds)
    {
        var specification = new FormElementBySpaceSpecification(spaceId)
            .And(new FormElementByVersionSpecification(versionId))
            .And(new FormElementByIncludedIdsSpecification(elementIds));
        
        var formElements = await _formElementRepository.FilterAsync(specification);
        if (formElements.Count != elementIds.Count)
        {
            var notFoundId = elementIds.Except(formElements.Select(x => x.Id)).First();
            throw new FormElementNotFoundException(notFoundId, versionId, spaceId);
        }
        
        return formElements.ToList();
    }
}