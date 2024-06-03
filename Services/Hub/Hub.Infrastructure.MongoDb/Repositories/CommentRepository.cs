using BuildingBlocks.Application.Identity;
using BuildingBlocks.Infrastructure.MongoDb.Repositories;
using Hub.Domain.CommentAggregate.Entities;
using Hub.Domain.CommentAggregate.Enums;
using Hub.Domain.CommentAggregate.Repositories;
using MediatR;
using MongoDB.Driver;

namespace Hub.Infrastructure.MongoDb.Repositories;

public class CommentRepository : MongoDbRepository<Comment, Guid>, ICommentRepository
{
    public CommentRepository(IMongoDatabase database, ICurrentUser currentUser, IMediator mediator) 
        : base(database, currentUser, mediator)
    {
    }

    public Task<List<Comment>> GetSubmissionCommentsAsync(int submissionId, int page, int size)
    {
        var filter = Builders<Comment>.Filter.Eq(x => x.EntityId, submissionId.ToString())
                     & Builders<Comment>.Filter.Eq(x => x.EntityType, CommentEntity.Submission)
                     & Builders<Comment>.Filter.Eq(x => x.TenantId, CurrentUser.TenantId);
        
        var sort = Builders<Comment>.Sort.Descending(x => x.Created);
        
        return Collection.Find(filter)
            .Sort(sort)
            .Skip((page - 1) * size)
            .Limit(size)
            .ToListAsync();
    }

    public async Task<int> GetSubmissionCommentsCountAsync(int submissionId)
    {
        var filter = Builders<Comment>.Filter.Eq(x => x.EntityId, submissionId.ToString())
                     & Builders<Comment>.Filter.Eq(x => x.EntityType, CommentEntity.Submission)
                     & Builders<Comment>.Filter.Eq(x => x.TenantId, CurrentUser.TenantId);
        
        var total = await Collection.CountDocumentsAsync(filter);

        return (int)total;
    }
}