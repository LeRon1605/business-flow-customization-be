using BuildingBlocks.Application.Cqrs;
using Submission.Application.UseCases.Submissions.Dtos;

namespace Submission.Application.UseCases.Submissions.Commands;

public class SubmitFormExternalCommand : ICommand<int>
{
    public ExternalSubmitFormDto Data { get; set; }
    
    public SubmitFormExternalCommand(ExternalSubmitFormDto data)
    {
        Data = data;
    }
}