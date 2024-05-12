using Domain.Enums;
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

    public SubmissionDomainService(IFormSubmissionRepository submissionRepository
        , ISpaceBusinessFlowRepository spaceBusinessFlowRepository
        , ISubmissionFieldCreatorFactory submissionFieldCreatorFactory)
    {
        _submissionRepository = submissionRepository;
        _spaceBusinessFlowRepository = spaceBusinessFlowRepository;
        _submissionFieldCreatorFactory = submissionFieldCreatorFactory;
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

        foreach (var field in submissionModel.Fields)
        {
            var creator = _submissionFieldCreatorFactory.Get(FormElementType.Number);
            var submissionField = creator.Create(field);

            submission.AddField(submissionField);
        }

        await _submissionRepository.InsertAsync(submission);

        return submission;
    }
}