using Application.Dtos.Submissions.Requests;
using AutoMapper;
using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Data;
using BusinessFlow.Application.Clients.Abstracts;
using BusinessFlow.Application.UseCases.BusinessFlows.Dtos;
using BusinessFlow.Domain.BusinessFlowAggregate.DomainServices.Interfaces;
using BusinessFlow.Domain.BusinessFlowAggregate.Models;
using BusinessFlow.Domain.SpaceAggregate.Entities;
using BusinessFlow.Domain.SpaceAggregate.Exceptions;
using BusinessFlow.Domain.SpaceAggregate.Repositories;
using Microsoft.Extensions.Logging;

namespace BusinessFlow.Application.UseCases.BusinessFlows.Commands;

public class UpdateSpaceBusinessFlowCommandHandler : ICommandHandler<UpdateSpaceBusinessFlowCommand, int>
{
    private readonly ISpaceRepository _spaceRepository;
    private readonly IBusinessFlowDomainService _businessFlowDomainService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ISubmissionClient _submissionClient;
    private readonly ILogger<UpdateSpaceBusinessFlowCommandHandler> _logger;
    
    public UpdateSpaceBusinessFlowCommandHandler(ISpaceRepository spaceRepository
        , IBusinessFlowDomainService businessFlowDomainService
        , IUnitOfWork unitOfWork
        , IMapper mapper
        , ISubmissionClient submissionClient
        , ILogger<UpdateSpaceBusinessFlowCommandHandler> logger)
    {
        _spaceRepository = spaceRepository;
        _businessFlowDomainService = businessFlowDomainService;
        _unitOfWork = unitOfWork;
        _submissionClient = submissionClient;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<int> Handle(UpdateSpaceBusinessFlowCommand request, CancellationToken cancellationToken)
    {
        var space = await _spaceRepository.FindByIdAsync(request.Id);
        if (space == null)
        {
            throw new SpaceNotFoundException(request.Id);
        }

        var blockModel = _mapper.Map<List<BusinessFlowBlockModel>>(request.Blocks);
        var branchModel = _mapper.Map<List<BusinessFlowBranchModel>>(request.Branches);
        var businessFlowModel = BusinessFlowModel.Create(blockModel, branchModel);

        try
        {
            await _unitOfWork.BeginTransactionAsync();
            
            var businessFlowVersion = await _businessFlowDomainService.CreateAsync(space, businessFlowModel.BusinessFlowModel);

            await _unitOfWork.CommitAsync();
            
            await CreateBusinessFlowBlockFormsAsync(space, businessFlowModel.MappingGeneratedIds, request.Blocks);

            _logger.LogInformation("New business flow version created successfully. Id: {Id}", businessFlowVersion.Id);

            await _unitOfWork.CommitTransactionAsync();

            return businessFlowVersion.Id;
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
    
    private async Task CreateBusinessFlowBlockFormsAsync(Space space
        , Dictionary<Guid, Guid> mappingGeneratedIds
        , List<BusinessFlowBlockRequestDto> blocks)
    {
        var tasks = new List<Task>();
        
        foreach (var block in blocks)
        {
            if (!block.Elements.Any()) 
                continue;
            
            var request = new FormRequestDto()
            {
                BusinessFlowBlockId = mappingGeneratedIds[block.Id],
                Name = block.Name,
                Elements = block.Elements,
                CoverImageUrl = "default"
            };
            
            tasks.Add(_submissionClient.CreateFormAsync(space.Id, request));
        }

        await Task.WhenAll(tasks);
    }
}