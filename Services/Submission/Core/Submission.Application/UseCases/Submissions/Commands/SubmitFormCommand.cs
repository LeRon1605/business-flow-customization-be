using BuildingBlocks.Application.Cqrs;
using Submission.Application.UseCases.Submissions.Dtos;

namespace Submission.Application.UseCases.Submissions.Commands;

public class SubmitFormCommand : ICommand<int>
{
    public int SpaceId { get; set; }
    
    public SubmitFormDto Data { get; set; }

    public SubmitFormCommand(int spaceId, SubmitFormDto data)
    {
        SpaceId = spaceId;
        Data = data;
    }
}