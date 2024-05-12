using Application.Dtos.Submissions.Requests;
using BuildingBlocks.Application.Cqrs;

namespace Submission.Application.UseCases.Forms.Commands;

public class UpdateFormCommand : ICommand<int>
{
    public int SpaceId { get; set; }
    
    public FormRequestDto FormDto { get; set; }
    
    public UpdateFormCommand(int spaceId, FormRequestDto formDto)
    {
        SpaceId = spaceId;
        FormDto = formDto;
    }
}