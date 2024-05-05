using BuildingBlocks.Application.Cqrs;
using BusinessFlow.Domain.BusinessFlowAggregate.DomainServices.Interfaces;
using BusinessFlow.Domain.BusinessFlowAggregate.Models;

namespace BusinessFlow.Application.UseCases.BusinessFlows.Commands;

public class ValidateBusinessFlowCommandHandler : ICommandHandler<ValidateBusinessFlowCommand, List<BusinessFlowBlockValidationModel>>
{
    private readonly IBusinessFlowValidationDomainService _businessFlowValidationDomainService;
    
    public ValidateBusinessFlowCommandHandler(IBusinessFlowValidationDomainService businessFlowValidationDomainService)
    {
        _businessFlowValidationDomainService = businessFlowValidationDomainService;
    }

    public async Task<List<BusinessFlowBlockValidationModel>> Handle(ValidateBusinessFlowCommand request, CancellationToken cancellationToken)
    {
        return _businessFlowValidationDomainService.Validate(request.Blocks, request.Branches);
    }
}