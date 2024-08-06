using System.Numerics;
using BuildingBlocks.Infrastructure.Cdc.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace BuildingBlocks.Infrastructure.Cdc.States;

public class CdcStateService : ICdcStateService
{
    protected readonly IMongoDatabase Database;
    protected readonly IMongoCollection<CaptureTableStateModel> Collection;
    
    public CdcStateService(IConfiguration configuration)
    {
        Database = new MongoClient(configuration.GetConnectionString("MongoDb")).GetDatabase("Shared");
        Collection = Database.GetCollection<CaptureTableStateModel>("capture_table_state");
    }
    
    public async Task<CaptureTableStateModel?> GetLastProcessedLsnAsync(string name, CancellationToken cancellationToken)
    {
        return await Collection.AsQueryable()
            .Where(x => x.Name == name)
            .FirstOrDefaultAsync(cancellationToken);
    }
    
    public async Task SaveLastProcessedLsnAsync(string name, BigInteger lastLsn, CancellationToken cancellationToken)
    {
        var isExisted = await Collection.AsQueryable()
            .AnyAsync(x => x.Name == name, cancellationToken);
        if (isExisted)
            return;
        
        var state = new CaptureTableStateModel() { Id = Guid.NewGuid(), Name = name, LastProcessedLsn = lastLsn };
        await Collection.InsertOneAsync(state, cancellationToken);
    }

    public Task UpdateLastProcessedLsnAsync(Guid id, BigInteger lastLsn, CancellationToken cancellationToken)
    {
        return Collection.UpdateOneAsync(x => x.Id == id
            , Builders<CaptureTableStateModel>.Update.Set(x => x.LastProcessedLsn, lastLsn)
            , cancellationToken: cancellationToken);
    }
}