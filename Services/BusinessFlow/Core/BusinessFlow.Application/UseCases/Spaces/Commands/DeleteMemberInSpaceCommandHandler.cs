using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Data;
using BusinessFlow.Domain.SpaceAggregate.Entities;
using BusinessFlow.Domain.SpaceAggregate.Exceptions;
using BusinessFlow.Domain.SpaceAggregate.Repositories;

namespace BusinessFlow.Application.UseCases.Spaces.Commands;

public class DeleteMemberInSpaceCommandHandler: ICommandHandler<DeleteMemberInSpaceCommand>
{
    private readonly ISpaceRepository _spaceRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public DeleteMemberInSpaceCommandHandler(ISpaceRepository spaceRepository, IUnitOfWork unitOfWork)
    {
        _spaceRepository = spaceRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(DeleteMemberInSpaceCommand request, CancellationToken cancellationToken)
    {
        var space = await _spaceRepository.FindByIdAsync(request.SpaceId, $"{nameof(Space.Members)}.{nameof(SpaceMember.Role)}");
        if (space == null)
        {
            throw new SpaceNotFoundException(request.SpaceId);
        }
        
        space.RemoveMember(request.UserId);
        await _unitOfWork.CommitAsync();
    }
}