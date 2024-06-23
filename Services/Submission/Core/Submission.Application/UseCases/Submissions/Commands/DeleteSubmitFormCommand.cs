using BuildingBlocks.Application.Cqrs;

namespace Submission.Application.UseCases.Submissions.Commands;

public class DeleteSubmitFormCommand : ICommand
{
    public int SubmissionId { get; set; }
    
    public DeleteSubmitFormCommand(int submissionId)
    {
        SubmissionId = submissionId;
    }
}