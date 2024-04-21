using Amazon.S3;
using Amazon.S3.Model;
using BuildingBlocks.Application.FileUploader;
using BuildingBlocks.Application.Schedulers;

namespace BuildingBlocks.Infrastructure.CloudflareR2;

public class CloudflareFileUploader : IFileUploader
{
    private readonly CloudflareSetting _cloudflareSetting;
    private readonly IBackGroundJobManager _backGroundJobManager;
    private readonly AmazonS3Client _s3Client;
    
    public CloudflareFileUploader(CloudflareSetting cloudflareSetting, IBackGroundJobManager backGroundJobManager)
    {
        _cloudflareSetting = cloudflareSetting;
        _backGroundJobManager = backGroundJobManager;
        _s3Client = new AmazonS3Client(_cloudflareSetting.AccessKey
            , _cloudflareSetting.AccessSecret
            , new AmazonS3Config
            {
                ServiceURL = $"https://{_cloudflareSetting.AccountId}.r2.cloudflarestorage.com"
            });
    }
    
    public async Task<UploaderResult> UploadAsync(string name, Stream stream, string contentType)
    {
        name = $"{Guid.NewGuid()}-{name}";
        var request = new PutObjectRequest
        {
            BucketName = _cloudflareSetting.BucketName,
            Key = name,
            InputStream = stream,
            ContentType = contentType,
            DisablePayloadSigning = true
        };

        var response = await _s3Client.PutObjectAsync(request);

        if (response.HttpStatusCode != System.Net.HttpStatusCode.OK && response.HttpStatusCode != System.Net.HttpStatusCode.Accepted)
        {
            throw new Exception("Upload to Cloudflare R2 failed");
        }
        
        return UploaderResult.Success($"{_cloudflareSetting.DevAccessUrl}/{name}");
    }

    public void PushUpload(string name, Stream stream, string contentType)
    {
        _backGroundJobManager.Fire(() => UploadAsync(name, stream, contentType));
    }
}