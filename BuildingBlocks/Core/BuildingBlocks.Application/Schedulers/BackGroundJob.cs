namespace BuildingBlocks.Application.Schedulers;

public class BackGroundJob : IBackGroundJob
{
    public string? UserId { get; set; }
    public int? TenantId { get; set; }
}