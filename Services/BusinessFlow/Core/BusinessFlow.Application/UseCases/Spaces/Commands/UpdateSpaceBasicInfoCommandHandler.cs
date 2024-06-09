using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Identity;
using BusinessFlow.Domain.SpaceAggregate.DomainServices;
using BusinessFlow.Domain.SpaceAggregate.Exceptions;
using BusinessFlow.Domain.SpaceAggregate.Repositories;
using Microsoft.Extensions.Logging;

namespace BusinessFlow.Application.UseCases.Spaces.Commands;

public class UpdateSpaceBasicInfoCommandHandler : ICommandHandler<UpdateSpaceBasicInfoCommand>
{
    private readonly ISpaceDomainService _spaceDomainService;
    private readonly ISpaceRepository _spaceRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUser _currentUser;
    private readonly ILogger<CreateSpaceCommandHandler> _logger;
    
    public UpdateSpaceBasicInfoCommandHandler(ISpaceDomainService spaceDomainService
        , ISpaceRepository spaceRepository
        , IUnitOfWork unitOfWork
        , ICurrentUser currentUser
        , ILogger<CreateSpaceCommandHandler> logger)
    {
        _spaceDomainService = spaceDomainService;
        _spaceRepository = spaceRepository;
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
        _logger = logger;
    }
    
    public async Task Handle(UpdateSpaceBasicInfoCommand request, CancellationToken cancellationToken)
    {
        var space = await _spaceRepository.FindByIdAsync(request.Id);
        if (space == null)
        {
            throw new SpaceNotFoundException(request.Id);
        }

        await _spaceDomainService.UpdateBasicInfoAsync(
            space,
            request.Name,
            request.Description,
            request.Color);
       
        await _unitOfWork.CommitAsync();
        _logger.LogInformation("Updated space information with id '{SpaceID}'", space.Id);
    }
}