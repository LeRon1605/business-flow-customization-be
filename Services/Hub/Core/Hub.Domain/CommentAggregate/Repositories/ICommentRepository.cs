using BuildingBlocks.Domain.Repositories;
using Hub.Domain.CommentAggregate.Entities;

namespace Hub.Domain.CommentAggregate.Repositories;

public interface ICommentRepository : IRepository<Comment, Guid>
{
    Task<List<Comment>> GetSubmissionCommentsAsync(int submissionId, int page, int size);
    
    Task<int> GetSubmissionCommentsCountAsync(int submissionId);
}