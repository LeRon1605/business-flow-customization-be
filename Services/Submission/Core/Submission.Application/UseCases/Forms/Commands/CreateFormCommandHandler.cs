using AutoMapper;
using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Data;
using Microsoft.Extensions.Logging;
using Submission.Domain.FormAggregate.DomainServices;
using Submission.Domain.FormAggregate.Models;

namespace Submission.Application.UseCases.Forms.Commands;

public class CreateFormCommandHandler : ICommandHandler<CreateFormCommand>
{
    private readonly IFormDomainService _formDomainService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateFormCommandHandler> _logger;
    
    public CreateFormCommandHandler(IFormDomainService formDomainService
        , IUnitOfWork unitOfWork
        , IMapper mapper
        , ILogger<CreateFormCommandHandler> logger)
    {
        _formDomainService = formDomainService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task Handle(CreateFormCommand request, CancellationToken cancellationToken)
    {
        var formModel = _mapper.Map<FormModel>(request.FormDto);
        
        var form = await _formDomainService.CreateAsync(request.SpaceId, formModel);
        
        await _unitOfWork.CommitAsync();
        
        _logger.LogInformation("Form {FormId} is created for space {SpaceId}", form.Id, request.SpaceId);
    }
}