using AutoMapper;
using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Data;
using Submission.Domain.SubmissionAggregate.DomainServices.Abstracts;
using Submission.Domain.SubmissionAggregate.Models;

namespace Submission.Application.UseCases.Submissions.Commands;

public class UpdateSubmitFormFieldCommandHandler : ICommandHandler<UpdateSubmitFormFieldCommand>
{
    private readonly ISubmissionDomainService _submissionDomainService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public UpdateSubmitFormFieldCommandHandler(ISubmissionDomainService submissionDomainService
        , IUnitOfWork unitOfWork
        , IMapper mapper)
    {
        _submissionDomainService = submissionDomainService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task Handle(UpdateSubmitFormFieldCommand request, CancellationToken cancellationToken)
    {
        var fieldModel = _mapper.Map<SubmissionFieldModel>(request.Field);
        
        await _submissionDomainService.UpdateAsync(request.SubmissionId, fieldModel);
        
        await _unitOfWork.CommitAsync();
    }
}