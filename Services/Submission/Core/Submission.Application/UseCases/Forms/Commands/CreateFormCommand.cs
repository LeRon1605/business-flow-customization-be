using Application.Dtos.Submissions.Requests;
using BuildingBlocks.Application.Cqrs;

namespace Submission.Application.UseCases.Forms.Commands;

public class CreateFormCommand : ICommand
{
    public int SpaceId { get; set; }
    
    public CreateFormRequestDto FormDto { get; set; }
    
    public CreateFormCommand(int spaceId, CreateFormRequestDto formDto)
    {
        SpaceId = spaceId;
        FormDto = formDto;
    }
}