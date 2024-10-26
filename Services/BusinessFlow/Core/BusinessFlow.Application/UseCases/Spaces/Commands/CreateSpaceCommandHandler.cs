﻿using Application.Dtos.Submissions.Requests;
using AutoMapper;
using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Dtos;
using BuildingBlocks.Application.Identity;
using BusinessFlow.Application.Clients.Abstracts;
using BusinessFlow.Application.UseCases.BusinessFlows.Dtos;
using BusinessFlow.Domain.BusinessFlowAggregate.DomainServices.Interfaces;
using BusinessFlow.Domain.BusinessFlowAggregate.Entities;
using BusinessFlow.Domain.BusinessFlowAggregate.Models;
using BusinessFlow.Domain.BusinessFlowAggregate.Repositories;
using BusinessFlow.Domain.SpaceAggregate.DomainServices;
using BusinessFlow.Domain.SpaceAggregate.Entities;
using Microsoft.Extensions.Logging;

namespace BusinessFlow.Application.UseCases.Spaces.Commands;

public class CreateSpaceCommandHandler : ICommandHandler<CreateSpaceCommand, int>
{
    private readonly ISpaceDomainService _spaceDomainService;
    private readonly IBusinessFlowDomainService _businessFlowDomainService;
    private readonly IBusinessFlowVersionRepository _businessFlowVersionRepository;
    private readonly ICurrentUser _currentUser;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISubmissionClient _submissionClient;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateSpaceCommandHandler> _logger;
    
    public CreateSpaceCommandHandler(ISpaceDomainService spaceDomainService
        , IBusinessFlowDomainService businessFlowDomainService
        , IBusinessFlowVersionRepository businessFlowVersionRepository
        , ISubmissionClient submissionClient
        , ICurrentUser currentUser
        , IUnitOfWork unitOfWork
        , IMapper mapper
        , ILogger<CreateSpaceCommandHandler> logger)
    {
        _spaceDomainService = spaceDomainService;
        _businessFlowDomainService = businessFlowDomainService;
        _businessFlowVersionRepository = businessFlowVersionRepository;
        _submissionClient = submissionClient;
        _currentUser = currentUser;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<int> Handle(CreateSpaceCommand request, CancellationToken cancellationToken)
    {
        var space = await _spaceDomainService.CreateAsync(request.Name, request.Description, request.Color, _currentUser.Id);

        try
        {
            await _unitOfWork.BeginTransactionAsync();
        
            var blockModel = _mapper.Map<List<BusinessFlowBlockModel>>(request.Blocks);
            var branchModel = _mapper.Map<List<BusinessFlowBranchModel>>(request.Branches);
            var businessFlowModel = BusinessFlowModel.Create(blockModel, branchModel);
            
            var businessFlowVersion = await _businessFlowDomainService.CreateAsync(space, businessFlowModel.BusinessFlowModel);
        
            await _unitOfWork.CommitAsync();
        
            _logger.LogInformation("Space {SpaceName} created", space.Name);

            await _submissionClient.CreateFormAsync(space.Id, request.Form);
            
            await CreateBusinessFlowBlockFormsAsync(space, businessFlowModel.MappingGeneratedIds, request.Blocks, businessFlowVersion);

            await _unitOfWork.CommitTransactionAsync();

            return space.Id;
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
    
    private async Task CreateBusinessFlowBlockFormsAsync(Space space
        , Dictionary<Guid, Guid> mappingGeneratedIds
        , List<BusinessFlowBlockRequestDto> blocks
        , BusinessFlowVersion businessFlowVersion)
    {
        var forms = new Dictionary<Guid, Task<SimpleIdResponse<int>>>();
        
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
            
            forms.Add(mappingGeneratedIds[block.Id], _submissionClient.CreateFormAsync(space.Id, request));
        }

        await Task.WhenAll(forms.Select(x => x.Value));

        foreach (var form in forms)
        {
            var block = businessFlowVersion.Blocks.FirstOrDefault(b => b.Id == form.Key);
            if (block == null)
                continue;
            
            block.SetFormId(form.Value.Result.Id);
        }
        
        _businessFlowVersionRepository.Update(businessFlowVersion);
        await _unitOfWork.CommitAsync();
    }
}