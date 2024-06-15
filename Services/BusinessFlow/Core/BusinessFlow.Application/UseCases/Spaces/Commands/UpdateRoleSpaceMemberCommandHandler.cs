using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Data;
using BusinessFlow.Domain.SpaceAggregate.Exceptions;
using BusinessFlow.Domain.SpaceAggregate.Repositories;

namespace BusinessFlow.Application.UseCases.Spaces.Commands;

public class UpdateRoleSpaceMemberCommandHandler : ICommandHandler<UpdateRoleSpaceMemberCommand>
{
    private readonly ISpaceRepository _spaceRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public UpdateRoleSpaceMemberCommandHandler(ISpaceRepository spaceRepository, IUnitOfWork unitOfWork)
    {
        _spaceRepository = spaceRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(UpdateRoleSpaceMemberCommand request, CancellationToken cancellationToken)
    {
        var space = await _spaceRepository.FindByIdAsync(request.SpaceId);
        if (space == null)
        {
            throw new SpaceNotFoundException(request.SpaceId);
        }
        space.UpdateRoleSpaceMember(request.UserId, request.RoleId);
        await _unitOfWork.CommitAsync();
    }
}