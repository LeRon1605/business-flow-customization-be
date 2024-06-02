using AutoMapper;
using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Dtos;
using Hub.Application.UseCases.Comments.Dtos;
using Hub.Domain.CommentAggregate.Repositories;

namespace Hub.Application.UseCases.Comments.Queries;

public class GetSubmissionCommentQueryHandler : IQueryHandler<GetSubmissionCommentQuery, PagedResultDto<CommentDto>>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;
    
    public GetSubmissionCommentQueryHandler(ICommentRepository commentRepository
        , IMapper mapper)
    {
        _commentRepository = commentRepository;
        _mapper = mapper;
    }
    
    public async Task<PagedResultDto<CommentDto>> Handle(GetSubmissionCommentQuery request, CancellationToken cancellationToken)
    {
        var comments = await _commentRepository.GetSubmissionCommentsAsync(request.SubmissionId, request.Page, request.Size);
        var total = await _commentRepository.GetSubmissionCommentsCountAsync(request.SubmissionId);

        return new PagedResultDto<CommentDto>(total, request.Size, _mapper.Map<List<CommentDto>>(comments));
    }
}