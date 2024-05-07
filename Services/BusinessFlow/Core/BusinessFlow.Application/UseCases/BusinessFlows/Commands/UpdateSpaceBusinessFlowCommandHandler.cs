using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Data;
using BusinessFlow.Domain.BusinessFlowAggregate.DomainServices.Interfaces;
using BusinessFlow.Domain.BusinessFlowAggregate.Entities;
using BusinessFlow.Domain.SpaceAggregate.Exceptions;
using BusinessFlow.Domain.SpaceAggregate.Repositories;
using Microsoft.Extensions.Logging;

namespace BusinessFlow.Application.UseCases.BusinessFlows.Commands;

public class UpdateSpaceBusinessFlowCommandHandler : ICommandHandler<UpdateSpaceBusinessFlowCommand, int>
{
    private readonly ISpaceRepository _spaceRepository;
    private readonly IBusinessFlowDomainService _businessFlowDomainService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateSpaceBusinessFlowCommandHandler> _logger;
    
    public UpdateSpaceBusinessFlowCommandHandler(ISpaceRepository spaceRepository
        , IBusinessFlowDomainService businessFlowDomainService
        , IUnitOfWork unitOfWork
        , ILogger<UpdateSpaceBusinessFlowCommandHandler> logger)
    {
        _spaceRepository = spaceRepository;
        _businessFlowDomainService = businessFlowDomainService;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    
    public async Task<int> Handle(UpdateSpaceBusinessFlowCommand request, CancellationToken cancellationToken)
    {
        var space = await _spaceRepository.FindByIdAsync(request.Id);
        if (space == null)
        {
            throw new SpaceNotFoundException(request.Id);
        }

        var businessFlowVersion = await _businessFlowDomainService.CreateAsync(space, new BusinessFlowModel(request.Blocks, request.Branches));
        
        await _unitOfWork.CommitAsync();

        _logger.LogInformation("New business flow version created successfully. Id: {Id}", businessFlowVersion.Id);
        
        return businessFlowVersion.Id;
    }
}