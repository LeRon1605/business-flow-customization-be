namespace BuildingBlocks.Application.Data;

public interface IDataSeeder
{
    int Id { get; }
    
    Task SeedAsync();
}