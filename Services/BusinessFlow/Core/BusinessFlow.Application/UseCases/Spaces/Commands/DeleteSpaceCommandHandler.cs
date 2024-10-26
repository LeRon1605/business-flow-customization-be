﻿using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Data;
using BusinessFlow.Domain.BusinessFlowAggregate.Repositories;
using BusinessFlow.Domain.SpaceAggregate.DomainServices;
using BusinessFlow.Domain.SpaceAggregate.Exceptions;
using BusinessFlow.Domain.SpaceAggregate.Repositories;

namespace BusinessFlow.Application.UseCases.Spaces.Commands;

public class DeleteSpaceCommandHandler : ICommandHandler<DeleteSpaceCommand>
{
    private readonly ISpaceRepository _spaceRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public DeleteSpaceCommandHandler(ISpaceRepository spaceRepository, IUnitOfWork unitOfWork)
    {
        _spaceRepository = spaceRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(DeleteSpaceCommand request, CancellationToken cancellationToken)
    {
        var space = await _spaceRepository.FindByIdAsync(request.SpaceId);
        if (space == null)
        {
            throw new SpaceNotFoundException(request.SpaceId);
        }

        _spaceRepository.Delete(space);
        await _unitOfWork.CommitAsync();
    }
}