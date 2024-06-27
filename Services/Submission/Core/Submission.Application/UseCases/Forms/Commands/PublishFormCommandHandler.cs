using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Data;
using Submission.Domain.FormAggregate.DomainServices;
using Submission.Domain.FormAggregate.Exceptions;
using Submission.Domain.FormAggregate.Repositories;

namespace Submission.Application.UseCases.Forms.Commands;

public class PublishFormCommandHandler : ICommandHandler<PublishFormCommand>
{
    private readonly IFormRepository _formRepository;
    private readonly IFormDomainService _formDomainService;
    private readonly IUnitOfWork _unitOfWork;
    
    public PublishFormCommandHandler(IFormRepository formRepository, IUnitOfWork unitOfWork, IFormDomainService formDomainService)
    {
        _formRepository = formRepository;
        _unitOfWork = unitOfWork;
        _formDomainService = formDomainService;
    }
    
    public async Task Handle(PublishFormCommand request, CancellationToken cancellationToken)
    {
        var form = await _formRepository.FindBySpaceIdAsync(request.SpaceId);
        if (form == null)
        {
            throw new SpaceFormNotFoundException(request.SpaceId);
        }

        await _formDomainService.PublishAsync(form, request.IsPublished);
        await _unitOfWork.CommitAsync();
    }
}