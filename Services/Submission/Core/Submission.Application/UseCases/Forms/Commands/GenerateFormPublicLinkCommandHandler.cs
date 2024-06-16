using BuildingBlocks.Application.Cqrs;
using Submission.Domain.FormAggregate.Exceptions;
using Submission.Domain.FormAggregate.Repositories;

namespace Submission.Application.UseCases.Forms.Commands;

public class GenerateFormPublicLinkCommandHandler : ICommandHandler<GenerateFormPublicLinkCommand, string>
{
    private readonly IFormRepository _formRepository;
    
    public GenerateFormPublicLinkCommandHandler(IFormRepository formRepository)
    {
        _formRepository = formRepository;
    }
    
    public async Task<string> Handle(GenerateFormPublicLinkCommand request, CancellationToken cancellationToken)
    {
        var form = await _formRepository.FindBySpaceIdAsync(request.SpaceId);
        if (form == null)
        {
            throw new SpaceFormNotFoundException(request.SpaceId);
        }

        return form.PublicToken;
    }
}