using AutoMapper;
using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Data;
using Submission.Domain.SubmissionAggregate.DomainServices.Abstracts;
using Submission.Domain.SubmissionAggregate.Models;

namespace Submission.Application.UseCases.Submissions.Commands;

public class SubmitFormCommandHandler : ICommandHandler<SubmitFormCommand, int>
{
    private readonly ISubmissionDomainService _submissionDomainService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SubmitFormCommandHandler(ISubmissionDomainService submissionDomainService
        , IUnitOfWork unitOfWork
        , IMapper mapper)
    {
        _submissionDomainService = submissionDomainService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    } 
    
    public async Task<int> Handle(SubmitFormCommand request, CancellationToken cancellationToken)
    {
        var submissionModel = _mapper.Map<SubmissionModel>(request.Data);
        
        var submission = await _submissionDomainService.CreateAsync(request.SpaceId, null, submissionModel);

        await _unitOfWork.CommitAsync();

        return submission.Id;
    }
}