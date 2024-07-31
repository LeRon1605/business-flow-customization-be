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
        await Collection.ReplaceOneAsync(x => x.Name == name
            , new CaptureTableStateModel() { Id = Guid.NewGuid().ToString(), Name = name, LastProcessedLsn = lastLsn }
            , new ReplaceOptions { IsUpsert = true }
            , cancellationToken);
    }
}