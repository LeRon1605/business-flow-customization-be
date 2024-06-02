using BuildingBlocks.Application.Cqrs;
using Hub.Application.UseCases.Comments.Dtos;

namespace Hub.Application.UseCases.Comments.Commands;

public class CreateSubmissionCommentCommand : ICommand<Guid>
{
    public int SubmissionId { get; set; }
    
    public CreateSubmissionCommentDto Comment { get; set; }
    
    public CreateSubmissionCommentCommand(int submissionId, CreateSubmissionCommentDto comment)
    {
        SubmissionId = submissionId;
        Comment = comment;
    }
}