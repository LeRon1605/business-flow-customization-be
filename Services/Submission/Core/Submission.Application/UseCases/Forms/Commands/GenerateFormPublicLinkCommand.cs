using System.Windows.Input;
using BuildingBlocks.Application.Cqrs;
using Submission.Application.UseCases.Dtos;
using ICommand = BuildingBlocks.Application.Cqrs.ICommand;

namespace Submission.Application.UseCases.Forms.Commands;

public class GenerateFormPublicLinkCommand: ICommand<FormPublishDto>
{
    public int SpaceId { get; set; }

    public GenerateFormPublicLinkCommand(int spaceId)
    {
        SpaceId = spaceId;
    }
}