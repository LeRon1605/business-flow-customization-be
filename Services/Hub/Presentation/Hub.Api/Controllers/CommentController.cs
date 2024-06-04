using BuildingBlocks.Application.Dtos;
using Hub.Application.UseCases.Comments.Commands;
using Hub.Application.UseCases.Comments.Dtos;
using Hub.Application.UseCases.Comments.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hub.Api.Controllers;

[Route("api/comments")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public CommentController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("submissions/{submissionId}")]
    [ProducesResponseType(typeof(PagedResultDto<CommentDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSubmissionComments(int submissionId, [FromQuery] PagingRequestDto dto)
    {
        var comments = await _mediator.Send(new GetSubmissionCommentQuery(submissionId, dto.Page, dto.Size));
        return Ok(comments);
    }
    
    [HttpPost("submissions/{submissionId}")]
    [ProducesResponseType(typeof(SimpleIdResponse<Guid>), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateSubmissionComment(int submissionId, [FromBody] CreateSubmissionCommentDto comment)
    {
        var commentId = await _mediator.Send(new CreateSubmissionCommentCommand(submissionId, comment));
        return Ok(SimpleIdResponse<Guid>.Create(commentId));
    }
    
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateComment(Guid id, [FromBody] UpdateCommentDto comment)
    {
        await _mediator.Send(new UpdateCommentCommand(id, comment));
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteComment(Guid id)
    {
        await _mediator.Send(new DeleteCommentCommand(id));
        return NoContent();
    }
}