using Application.Dtos.Notifications.Models;
using Application.Dtos.Notifications.Requests;
using AutoMapper;
using BuildingBlocks.Application.Identity;
using Domain.Enums;
using Hub.Application.Clients;
using Hub.Application.Clients.Abstracts;
using Hub.Application.Dtos;
using Hub.Application.Services.Abstracts;
using Hub.Domain.NotificationAggregate.DomainServices;
using Hub.Domain.NotificationAggregate.Enums;
using Hub.Domain.NotificationAggregate.Models;
using Hub.Infrastructure.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace Hub.Infrastructure.SignalR.Services;

public class NotificationSenderService : INotificationSenderService
{
    private readonly ICurrentUser _currentUser;
    private readonly IHubContext<NotificationHub> _hubContext;
    private readonly IConnectionManager _connectionManager;
    private readonly INotificationDomainService _notificationDomainService;
    private readonly ITemplateGenerator _templateGenerator;
    private readonly IInternalSubmissionClient _internalSubmissionClient;
    private readonly IInternalBusinessFlowClient _internalBusinessFlowClient;
    private readonly IMapper _mapper;
    
    public NotificationSenderService(ICurrentUser currentUser
        , IHubContext<NotificationHub> hubContext
        , ITemplateGenerator templateGenerator
        , IConnectionManager connectionManager
        , INotificationDomainService notificationDomainService
        , IInternalSubmissionClient internalSubmissionClient
        , IInternalBusinessFlowClient internalBusinessFlowClient
        , IMapper mapper)
    {
        _currentUser = currentUser;
        _hubContext = hubContext;
        _templateGenerator = templateGenerator;
        _connectionManager = connectionManager;
        _notificationDomainService = notificationDomainService;
        _internalSubmissionClient = internalSubmissionClient;
        _internalBusinessFlowClient = internalBusinessFlowClient;
        _mapper = mapper;
    }

    public async Task SendAsync(string receiverId
        , int tenantId
        , string data
        , NotificationType type)
    {
        var templateData = await GenerateTemplateAsync(type, data);
        if (templateData == null)
            return;
        
        var notification = await _notificationDomainService.CreateAsync(templateData.Title
            , templateData.Content
            , type
            , _currentUser.Id
            , receiverId
            , templateData.MetaData);
        
        var connections = _connectionManager.GetConnections(receiverId, tenantId);
        if (!connections.Any())
            return;
        
        await _hubContext.Clients.Clients(connections).SendAsync("NotificationMessage", _mapper.Map<NotificationModel>(notification));
    }

    public async Task SendAsync(List<string> userIds
        , int tenantId
        , string data
        , NotificationType type)
    {
        var templateData = await GenerateTemplateAsync(type, data);
        if (templateData == null)
            return;
        
        foreach (var userId in userIds)
        {
            var notification = await _notificationDomainService.CreateAsync(templateData.Title
                , templateData.Content
                , type
                , _currentUser.Id
                , userId
                , templateData.MetaData);
            
            var connections = _connectionManager.GetConnections(userId, tenantId);
            if (!connections.Any())
                continue;
            
            await _hubContext.Clients.Clients(connections).SendAsync("NotificationMessage", _mapper.Map<NotificationModel>(notification));
        }
    }

    public async Task SendRealTimeAsync(string receiverId, int tenantId, string data, NotificationType type)
    {
        var templateData = await GenerateTemplateAsync(type, data);
        if (templateData == null)
            return;
        
        var notification = new NotificationModel()
        {
            Id = Guid.NewGuid(),
            Title = templateData.Title,
            Content = templateData.Content,
            Type = type,
            Status = NotificationStatus.UnRead,
            SenderId = _currentUser.Id,
            ReceiverId = receiverId,
            MetaData = JsonConvert.SerializeObject(templateData.MetaData)
        };
        
        var connections = _connectionManager.GetConnections(receiverId, tenantId);
        if (!connections.Any())
            return;
        
        await _hubContext.Clients.Clients(connections).SendAsync("RealTimeMessage", notification);
    }

    public async Task SendRealTimeAsync(List<string> userIds, int tenantId, string data, NotificationType type)
    {
        var templateData = await GenerateTemplateAsync(type, data);
        if (templateData == null)
            return;
        
        foreach (var userId in userIds)
        {
            var notification = new NotificationModel()
            {
                Id = Guid.NewGuid(),
                Title = templateData.Title,
                Content = templateData.Content,
                Type = type,
                Status = NotificationStatus.UnRead,
                SenderId = _currentUser.Id,
                ReceiverId = userId,
                MetaData = JsonConvert.SerializeObject(templateData.MetaData)
            };
            
            var connections = _connectionManager.GetConnections(userId, tenantId);
            if (!connections.Any())
                continue;
            
            await _hubContext.Clients.Clients(connections).SendAsync("RealTimeMessage", _mapper.Map<NotificationModel>(notification));
        }
    }

    private async Task<NotificationTemplateDto?> GenerateTemplateAsync(NotificationType type, string data)
    {
        switch (type)
        {
            case NotificationType.PersonInChargeAssigned:
                return await GeneratePersonInChargeAssignedAsync(data);
            
            case NotificationType.SubmissionExecutionInitiated:
                return GetSubmissionExecutionInitiatedTemplate(data);
        }

        return null;
    }
    
    private async Task<NotificationTemplateDto?> GeneratePersonInChargeAssignedAsync(string data)
    {
        var model = JsonConvert.DeserializeObject<NotificationPersonInChargeAssignedModel>(data);
        if (model == null)
            return null;

        var submissions = await _internalSubmissionClient.GetSubmissionNotificationDataAsync(model.SpaceId
            , new List<int>() { model.SubmissionId });
        var businessFlows = await _internalBusinessFlowClient.GetBusinessFlowNotificationDataAsync(model.SpaceId
            , new List<Guid>() { model.BusinessFlowBlockId });
        
        var submission = submissions.FirstOrDefault();
        var businessFlow = businessFlows.FirstOrDefault(x => x.Type == BusinessFlowNotificationDataType.BusinessFlow);
        if (submission == null || businessFlow == null)
            return null;
        
        var titleData = new Dictionary<string, string>
        {
            { "RecordName", submission.Name }
        };
        
        var contentData = new Dictionary<string, string>
        {
            { "BusinessBlock", businessFlow.Name }
        };
        
        var title = _templateGenerator.GenerateNotificationTitle(NotificationType.PersonInChargeAssigned, titleData);
        var content = _templateGenerator.GenerateNotificationContent(NotificationType.PersonInChargeAssigned, contentData);
        
        return new NotificationTemplateDto
        {
            Title = title,
            Content = content,
            MetaData = new Dictionary<string, string>()
            {
                { "SpaceId",  model.SpaceId.ToString() },
                { "BusinessFlowBlockId", model.BusinessFlowBlockId.ToString() },
                { "SubmissionId", model.SubmissionId.ToString() }
            }
        };
    }
    
    private NotificationTemplateDto? GetSubmissionExecutionInitiatedTemplate(string data)
    {
        var model = JsonConvert.DeserializeObject<NotificationSubmissionExecutionInitiatedModel>(data);
        if (model == null)
            return null;
        
        return new NotificationTemplateDto
        {
            MetaData = new Dictionary<string, string>()
            {
                { "Id",  model.Id.ToString() },
                { "ExecutionId", model.ExecutionId.ToString() }
            }
        };
    }
}