using AutoMapper;
using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Data;
using Microsoft.Extensions.Logging;
using Submission.Domain.FormAggregate.DomainServices;
using Submission.Domain.FormAggregate.Exceptions;
using Submission.Domain.FormAggregate.Models;
using Submission.Domain.FormAggregate.Repositories;
using Submission.Domain.FormAggregate.Specifications;

namespace Submission.Application.UseCases.Forms.Commands;

public class UpdateFormCommandHandler : ICommandHandler<UpdateFormCommand, int>
{
    private readonly IFormRepository _formRepository;
    private readonly IFormDomainService _formDomainService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateFormCommandHandler> _logger;
    
    public UpdateFormCommandHandler(IFormRepository formRepository
        , IFormDomainService formDomainService
        , IUnitOfWork unitOfWork
        , IMapper mapper
        , ILogger<UpdateFormCommandHandler> logger)
    {
        _formRepository = formRepository;
        _formDomainService = formDomainService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<int> Handle(UpdateFormCommand request, CancellationToken cancellationToken)
    {
        var form = await _formRepository.FindAsync(new SpaceFormSpecification(request.SpaceId));
        if (form == null)
        {
            throw new SpaceFormNotFoundException(request.SpaceId);
        }
        
        var formModel = _mapper.Map<FormModel>(request.FormDto);
        var formVersion = await _formDomainService.UpdateAsync(form, formModel);
        
        await _unitOfWork.CommitAsync();
        
        _logger.LogInformation("Form {FormId} is updated for space {SpaceId}", form.Id, request.SpaceId);
        
        return formVersion.Id;
    }
}