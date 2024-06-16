using AutoMapper;
using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Identity;
using Domain.Identities;
using Submission.Application.UseCases.Dtos;
using Submission.Domain.FormAggregate.Exceptions;
using Submission.Domain.FormAggregate.Repositories;
using Submission.Domain.SubmissionAggregate.DomainServices.Abstracts;
using Submission.Domain.SubmissionAggregate.Models;

namespace Submission.Application.UseCases.Submissions.Commands;

public class SubmitFormExternalCommandHandler : ICommandHandler<SubmitFormExternalCommand, int>
{
    private readonly ISubmissionDomainService _submissionDomainService;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _currentUser;
    private readonly IFormVersionRepository _formVersionRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public SubmitFormExternalCommandHandler(ISubmissionDomainService submissionDomainService
        , IMapper mapper
        , ICurrentUser currentUser
        , IFormVersionRepository formVersionRepository
        , IUnitOfWork unitOfWork)
    {
        _submissionDomainService = submissionDomainService;
        _mapper = mapper;
        _currentUser = currentUser;
        _formVersionRepository = formVersionRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<int> Handle(SubmitFormExternalCommand request, CancellationToken cancellationToken)
    {
        var targetForm = await _formVersionRepository.GetLatestVersionByTokenAsync(request.Data.Token, new FormVersionDto());
        if (targetForm == null)
        {
            throw new FormNotFoundException();
        }

        _currentUser.IsAuthenticated = true;
        _currentUser.Id = AppUser.SystemUser;
        _currentUser.TenantId = targetForm.TenantId;
        
        var submissionModel = _mapper.Map<SubmissionModel>(request.Data);
        submissionModel.FormVersionId = targetForm.Id;
        
        var submission = await _submissionDomainService.CreateAsync(targetForm.SpaceId, null, submissionModel);

        await _unitOfWork.CommitAsync();

        return submission.Id;
    }
}