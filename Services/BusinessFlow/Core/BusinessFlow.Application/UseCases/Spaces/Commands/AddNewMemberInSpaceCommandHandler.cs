using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Data;
using BusinessFlow.Domain.SpaceAggregate.DomainServices;
using BusinessFlow.Domain.SpaceAggregate.Enums;
using BusinessFlow.Domain.SpaceAggregate.Exceptions;
using BusinessFlow.Domain.SpaceAggregate.Repositories;

namespace BusinessFlow.Application.UseCases.Spaces.Commands;

public class AddNewMemberInSpaceCommandHandler : ICommandHandler<AddNewMemberInSpaceCommand>
{
    private readonly ISpaceRepository _spaceRepository;
    private readonly ISpaceDomainService _spaceDomainService;
    private readonly IUnitOfWork _unitOfWork;
    
    public AddNewMemberInSpaceCommandHandler(ISpaceRepository spaceRepository, ISpaceDomainService spaceDomainService, IUnitOfWork unitOfWork)
    {
        _spaceRepository = spaceRepository;
        _spaceDomainService = spaceDomainService;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(AddNewMemberInSpaceCommand request, CancellationToken cancellationToken)
    {
        var space = await _spaceRepository.FindByIdAsync(request.SpaceId);
        if (space == null)
        {
            throw new SpaceNotFoundException(request.SpaceId);
        }
        await _spaceDomainService.AddMemberAsync(space, request.UserId, request.RoleId);
        await _unitOfWork.CommitAsync();
    }
}