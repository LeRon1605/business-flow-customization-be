using BuildingBlocks.Application.Cqrs;

namespace Submission.Application.UseCases.Submissions.Commands;

public class UpdateSubmitFormNameCommand : ICommand
{
    public int SubmissionId { get; set; }
    
    public string Name { get; set; }
    
    public UpdateSubmitFormNameCommand(int submissionId, string name)
    {
        SubmissionId = submissionId;
        Name = name;
    }
}