using BuildingBlocks.Application.Cqrs;
using Submission.Application.UseCases.Submissions.Dtos;

namespace Submission.Application.UseCases.Submissions.Commands;

public class UpdateSubmitFormFieldCommand : ICommand
{
    public int SubmissionId { get; set; }
    
    public SubmissionFieldDto Field { get; set; }
    
    public UpdateSubmitFormFieldCommand(int submissionId, SubmissionFieldDto field)
    {
        SubmissionId = submissionId;
        Field = field;
    }
}