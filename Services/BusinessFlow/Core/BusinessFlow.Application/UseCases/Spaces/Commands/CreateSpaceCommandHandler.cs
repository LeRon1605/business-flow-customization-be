using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Identity;
using BusinessFlow.Domain.BusinessFlowAggregate.DomainServices.Interfaces;
using BusinessFlow.Domain.BusinessFlowAggregate.Entities;
using BusinessFlow.Domain.SpaceAggregate.DomainServices;
using Microsoft.Extensions.Logging;

namespace BusinessFlow.Application.UseCases.Spaces.Commands;

public class CreateSpaceCommandHandler : ICommandHandler<CreateSpaceCommand, int>
{
    private readonly ISpaceDomainService _spaceDomainService;
    private readonly IBusinessFlowDomainService _businessFlowDomainService;
    private readonly ICurrentUser _currentUser;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateSpaceCommandHandler> _logger;
    
    public CreateSpaceCommandHandler(ISpaceDomainService spaceDomainService
        , IBusinessFlowDomainService businessFlowDomainService
        , ICurrentUser currentUser
        , IUnitOfWork unitOfWork
        , ILogger<CreateSpaceCommandHandler> logger)
    {
        _spaceDomainService = spaceDomainService;
        _businessFlowDomainService = businessFlowDomainService;
        _currentUser = currentUser;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    
    public async Task<int> Handle(CreateSpaceCommand request, CancellationToken cancellationToken)
    {
        var space = await _spaceDomainService.CreateAsync(request.Name, request.Description, request.Color, _currentUser.Id);

        await _businessFlowDomainService.CreateAsync(space, new BusinessFlowModel(request.Blocks, request.Branches));
        
        await _unitOfWork.CommitAsync();
        
        _logger.LogInformation("Space {SpaceName} created", space.Name);

        return space.Id;
    }
}