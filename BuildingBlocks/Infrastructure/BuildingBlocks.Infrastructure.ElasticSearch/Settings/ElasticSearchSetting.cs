namespace BuildingBlocks.Infrastructure.ElasticSearch.Settings;

public class ElasticSearchSetting
{
    public string UserName { get; set; } = null!;
    
    public string Password { get; set; } = null!;

    public string Url { get; set; } = null!;
}