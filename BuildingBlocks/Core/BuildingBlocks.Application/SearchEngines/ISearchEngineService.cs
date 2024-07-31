using BuildingBlocks.Application.SearchEngines.Models;
using BuildingBlocks.Kernel.Services;

namespace BuildingBlocks.Application.SearchEngines;

public interface ISearchEngineService<TDocument> : IScopedService where TDocument : BaseSearchModel
{
    Task<TDocument?> FindAsync(string id);
    
    Task<List<TDocument>> FindAllAsync();
    
    Task<bool> CreateIndexAsync();
    
    Task<bool> InsertAsync(TDocument obj);
    
    Task<bool> InsertManyAsync(IList<TDocument> objs);
    
    Task<bool> UpdateAsync(TDocument t);
    
    Task<bool> DeleteAsync(string id);
    
    Task<long> GetTotalCountAsync();
}