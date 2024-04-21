namespace BuildingBlocks.Infrastructure.CloudflareR2;

public class CloudflareSetting
{
    public string BucketName { get; set; } = null!;
    public string AccountId { get; set; } = null!;
    public string AccessKey { get; set; } = null!;
    public string AccessSecret { get; set; } = null!;
    public string DevAccessUrl { get; set; } = null!;
}