using Application.Dtos.Notifications.Requests;
using Application.Dtos.Notifications.Responses;
using AutoMapper;
using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Domain.Repositories;
using BusinessFlow.Application.UseCases.BusinessFlows.Dtos;
using BusinessFlow.Application.UseCases.Notifications.Dtos;
using BusinessFlow.Application.UseCases.Spaces.Dtos;
using BusinessFlow.Domain.BusinessFlowAggregate.Entities;
using BusinessFlow.Domain.SpaceAggregate.Entities;
using BusinessFlow.Domain.SubmissionExecutionAggregate.Entities;
using BusinessFlow.Domain.SubmissionExecutionAggregate.Enums;
using BusinessFlow.Domain.SubmissionExecutionAggregate.Repositories;
using BusinessFlow.Domain.SubmissionExecutionAggregate.Specifications;

namespace BusinessFlow.Application.UseCases.Notifications.Queries;

public class GetBusinessFlowNotificationDataQueryHandler : IQueryHandler<GetBusinessFlowNotificationDataQuery, List<BusinessFlowNotificationDataDto>>
{
    private readonly IBasicReadOnlyRepository<Space, int> _spaceRepository;
    private readonly IBasicReadOnlyRepository<BusinessFlowBlock, Guid> _businessFlowBlockRepository;
    private readonly IBasicReadOnlyRepository<SubmissionExecution, int> _businessFlowExecutionRepository;
    private readonly IMapper _mapper;
    
    public GetBusinessFlowNotificationDataQueryHandler(IBasicReadOnlyRepository<Space, int> spaceRepository
        , IBasicReadOnlyRepository<BusinessFlowBlock, Guid> businessFlowBlockRepository
        , IBasicReadOnlyRepository<SubmissionExecution, int> businessFlowExecutionRepository
        , IMapper mapper)
    {
        _spaceRepository = spaceRepository;
        _businessFlowBlockRepository = businessFlowBlockRepository;
        _businessFlowExecutionRepository = businessFlowExecutionRepository;
        _mapper = mapper;
    }
    
    public async Task<List<BusinessFlowNotificationDataDto>> Handle(GetBusinessFlowNotificationDataQuery request
        , CancellationToken cancellationToken)
    {
        var businessFlows = await GetBusinessFlowNotificationData(request);
        var submissionPersonInCharge = await GetExecutionPersonInChargeDataAsync(request);
        var spaces = await GetSpaceNotificationData(request.SpaceId);
        
        var result = new List<BusinessFlowNotificationDataDto>();
        if (spaces != null)
        {
            result.Add(spaces);
        }
        
        result.AddRange(businessFlows);
        result.AddRange(submissionPersonInCharge);
        
        return result;
    }
    
    private async Task<BusinessFlowNotificationDataDto?> GetSpaceNotificationData(int spaceId)
    {
        var space = await _spaceRepository.FindByIdAsync(spaceId, new SpaceQueryDto());
        if (space == null)
        {
            return null;
        }

        return _mapper.Map<BusinessFlowNotificationDataDto>(space);
    }
    
    private async Task<List<BusinessFlowNotificationDataDto>> GetBusinessFlowNotificationData(GetBusinessFlowNotificationDataQuery request)
    {
        var businessFlowIds = request.Entities
            .Where(x => x.Type == BusinessFlowNotificationDataType.BusinessFlow)
            .Select(x => Guid.Parse(x.Id))
            .ToList();
        if (!businessFlowIds.Any())
            return new List<BusinessFlowNotificationDataDto>();
        
        var businessFlows = await _businessFlowBlockRepository.FindByIncludedIdsAsync(businessFlowIds, new BasicBusinessFlowBlockDto());
        return _mapper.Map<List<BusinessFlowNotificationDataDto>>(businessFlows);
    }
    
    // private async Task<List<BusinessFlowNotificationDataDto>> GetExecutionNotificationData(GetBusinessFlowNotificationDataQuery request)
    // {
    //     var executionIds = request.Entities
    //         .Where(x => x.Type == BusinessFlowNotificationDataType.Execution)
    //         .Select(x => (int)x.Id)
    //         .ToList();
    //     
    //     var executions = await _businessFlowExecutionRepository.FindByIncludedIdsAsync(executionIds, new BasicBusinessFlowExecutionDto());
    //
    //     return _mapper.Map<List<BusinessFlowNotificationDataDto>>(executions);
    // }
    
    private async Task<List<BusinessFlowNotificationDataDto>> GetExecutionPersonInChargeDataAsync(GetBusinessFlowNotificationDataQuery request)
    {
        var submissionIds = request.Entities
            .Where(x => x.Type == BusinessFlowNotificationDataType.ExecutionPersonInCharge)
            .Select(x => int.Parse(x.Id))
            .ToList();
        if (!submissionIds.Any())
            return new List<BusinessFlowNotificationDataDto>();
        
        var specification = new SubmissionExecutionByIncludedSubmissionIdsSpecification(submissionIds)
            .And(new SubmissionExecutionByStatusSpecification(SubmissionExecutionStatus.InProgress));
        var personInCharges = await _businessFlowExecutionRepository
            .FilterAsync(specification, new PersonInChargeQueryDto());

        return _mapper.Map<List<BusinessFlowNotificationDataDto>>(personInCharges);
    }
}