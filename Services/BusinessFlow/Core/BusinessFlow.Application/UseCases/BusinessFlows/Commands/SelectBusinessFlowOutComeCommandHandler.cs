using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Identity;
using BusinessFlow.Domain.SubmissionExecutionAggregate.DomainServices;
using Microsoft.Extensions.Logging;

namespace BusinessFlow.Application.UseCases.BusinessFlows.Commands;

public class SelectBusinessFlowOutComeCommandHandler : ICommandHandler<SelectBusinessFlowOutComeCommand>
{
    private readonly IBusinessFlowExecutorDomainService _businessFlowExecutorDomainService;
    private readonly ICurrentUser _currentUser;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<SelectBusinessFlowOutComeCommandHandler> _logger;
    
    public SelectBusinessFlowOutComeCommandHandler(IBusinessFlowExecutorDomainService businessFlowExecutorDomainService
        , ICurrentUser currentUser
        , IUnitOfWork unitOfWork
        , ILogger<SelectBusinessFlowOutComeCommandHandler> logger)
    {
        _businessFlowExecutorDomainService = businessFlowExecutorDomainService;
        _currentUser = currentUser;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    
    public async Task Handle(SelectBusinessFlowOutComeCommand request, CancellationToken cancellationToken)
    {
        await _businessFlowExecutorDomainService.MoveNextAsync(request.SubmissionId, request.OutComeId, _currentUser.Id);

        await _unitOfWork.CommitAsync();
    }
}