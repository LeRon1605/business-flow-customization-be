namespace BuildingBlocks.Infrastructure.MongoDb.Settings;

public class MongoDbSetting
{
    public string Host { get; set; } = null!;
    public string Port { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
 
    public string ConnectionString => $"mongodb://{UserName}:{Password}@{Host}:{Port}";
}