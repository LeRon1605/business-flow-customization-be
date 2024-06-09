using AutoMapper;
using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Data;
using Microsoft.Extensions.Logging;
using Submission.Application.Services.Dtos;
using Submission.Domain.FormAggregate.DomainServices;
using Submission.Domain.FormAggregate.Exceptions;
using Submission.Domain.FormAggregate.Repositories;

namespace Submission.Application.UseCases.Forms.Commands;

public class GenerateFormPublicLinkCommandHandler : ICommandHandler<GenerateFormPublicLinkCommand, string>
{
    private readonly IFormRepository _formRepository;
    private readonly IFormDomainService _formDomainService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateFormCommandHandler> _logger; 
    private readonly PublicFormSetting _publicFormSetting;
    
    public GenerateFormPublicLinkCommandHandler(IFormRepository formRepository
        , IFormDomainService formDomainService
        , IUnitOfWork unitOfWork
        , ILogger<UpdateFormCommandHandler> logger
        , PublicFormSetting publicFormSetting)
    {
        _formRepository = formRepository;
        _formDomainService = formDomainService;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _publicFormSetting = publicFormSetting;
    }
    
    public async Task<string> Handle(GenerateFormPublicLinkCommand request, CancellationToken cancellationToken)
    {
        var form = await _formRepository.FindBySpaceIdAsync(request.SpaceId);
        if (form == null)
        {
            throw new SpaceFormNotFoundException(request.SpaceId);
        }
        var link = form.PublicLinkUrl;
        if (form.IsShared == false & link == null)
        {
            link = await _formDomainService.GeneratePublicLinkAsync(form, _publicFormSetting.BaseUrl);
            await _unitOfWork.CommitAsync();
        }
        _logger.LogInformation("Form {FormId} is shared!", form.Id);
        return link;
    }
}