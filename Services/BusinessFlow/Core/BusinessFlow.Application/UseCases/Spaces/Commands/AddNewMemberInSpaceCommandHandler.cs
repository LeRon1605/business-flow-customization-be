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
    private readonly IUnitOfWork _unitOfWork;
    
    public AddNewMemberInSpaceCommandHandler(ISpaceRepository spaceRepository, IUnitOfWork unitOfWork)
    {
        _spaceRepository = spaceRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(AddNewMemberInSpaceCommand request, CancellationToken cancellationToken)
    {
        var space = await _spaceRepository.FindByIdAsync(request.SpaceId);
        if (space == null)
        {
            throw new SpaceNotFoundException(request.SpaceId);
        }
        space.AddMember(request.UserId, SpaceRole.Viewer);
        await _unitOfWork.CommitAsync();
    }
}