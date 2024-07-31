using System.Reflection;
using BuildingBlocks.Application.SearchEngines;
using BuildingBlocks.Application.SearchEngines.Attributes;
using BuildingBlocks.Application.SearchEngines.Models;
using Nest;

namespace BuildingBlocks.Infrastructure.ElasticSearch.Services;

public class SearchEngineService<TDocument> : ISearchEngineService<TDocument> where TDocument : BaseSearchModel
{
    protected readonly IElasticClient Client;
    protected readonly string IndexName;
    protected readonly IIndexMapperProvider<TDocument> IndexMapperProvider;
    
    public SearchEngineService(IElasticClient client, IIndexMapperProvider<TDocument> indexMapperProvider)
    {
        Client = client;
        IndexName = typeof(TDocument).GetCustomAttribute<IndexNameAttribute>()?.Name 
                    ?? throw new Exception("IndexNameAttribute is missing");
        IndexMapperProvider = indexMapperProvider;
    }
    
    public async Task<TDocument?> FindAsync(string id)
    {
        var response = await Client.GetAsync(DocumentPath<TDocument>.Id(id).Index(IndexName));
        if (response.IsValid)
            return response.Source;

        return null;
    }

    public async Task<List<TDocument>> FindAllAsync()
    {
        var search = new SearchDescriptor<TDocument>(IndexName).MatchAll();
        var response = await Client.SearchAsync<TDocument>(search);

        if (!response.IsValid)
            throw new Exception(response.ServerError?.ToString(), response.OriginalException);

        return response.Hits.Select(hit => hit.Source).ToList();
    }

    public async Task<bool> CreateIndexAsync()
    {
        if (!(await Client.Indices.ExistsAsync(IndexName)).Exists)
        {
            await Client.Indices.CreateAsync(IndexName, c =>
            {
                c.Map(IndexMapperProvider.GetMapping());
                return c;
            });
        }
        
        return true;
    }

    public async Task<bool> InsertAsync(TDocument obj)
    {
        var response = await Client.IndexAsync(obj, descriptor => descriptor.Index(IndexName));

        if (!response.IsValid)
            throw new Exception(response.ServerError?.ToString(), response.OriginalException);

        return true;
    }

    public async Task<bool> InsertManyAsync(IList<TDocument> objs)
    {
        var response = await Client.IndexManyAsync(objs, IndexName);

        if (!response.IsValid)
            throw new Exception(response.ServerError?.ToString(), response.OriginalException);
        
        return true;
    }

    public async Task<bool> UpdateAsync(TDocument obj)
    {
        var response = await Client.UpdateAsync(
            DocumentPath<TDocument>.Id(obj.Id).Index(IndexName),
            p => p.Doc(obj)
        );

        if (!response.IsValid)
            throw new Exception(response.ServerError?.ToString(), response.OriginalException);

        return true;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var response = await Client.DeleteAsync(new DeleteRequest(IndexName, id));
        if (!response.IsValid)
            throw new Exception(response.ServerError?.ToString(), response.OriginalException);

        return true;
    }

    public async Task<long> GetTotalCountAsync()
    {
        var search = new SearchDescriptor<TDocument>(IndexName).MatchAll();
        var response = await Client.SearchAsync<TDocument>(search);

        if (!response.IsValid)
            throw new Exception(response.ServerError?.ToString(), response.OriginalException);

        return response.Total;
    }
}