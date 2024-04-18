using BuildingBlocks.Application.FileUploader;
using BuildingBlocks.Application.Schedulers;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Logging;
using Polly;

namespace BuildingBlocks.Infrastructure.Cloudinary;

public class CloudinaryFileUploader : IFileUploader
{
    private readonly CloudinaryDotNet.Cloudinary _cloudinary;
    private readonly IBackGroundJobManager _backGroundJobManager;
    private readonly ILogger<CloudinaryFileUploader> _logger;

    public CloudinaryFileUploader(CloudinaryDotNet.Cloudinary cloudinary
        , IBackGroundJobManager backGroundJobManager
        , ILogger<CloudinaryFileUploader> logger)
    {
        _cloudinary = cloudinary;
        _backGroundJobManager = backGroundJobManager;
        _logger = logger;
    }
    
    public async Task<UploaderResult> UploadAsync(string name, Stream stream)
    {
        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription($"{Guid.NewGuid()}-{name}", stream),
            UseFilename = true,
            UniqueFilename = false,
            Overwrite = false,
            Folder = "BusinessFLow"
        };

        var policy = Policy.Handle<Exception>()
            .WaitAndRetryAsync(5,retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                (ex, time) =>
                {
                    _logger.LogError("Upload file {name} to cloudinary failed: {error}", name, ex.Message);
                }
            );

        var errorMessage = string.Empty;
        var result = await policy.ExecuteAsync(async () =>
        {
            var result = await _cloudinary.UploadAsync(uploadParams);
            if (result.Error == null)
            {
                return UploaderResult.Success(result.Url.ToString());
            }

            errorMessage = result.Error.Message;
            throw new Exception(result.Error.Message);
        });

        return result.IsSuccess ? result : UploaderResult.Failed(errorMessage);
    }

    public void PushUpload(string name, Stream stream)
    {
        _backGroundJobManager.Fire(() => UploadAsync(name, stream));
    }
}