namespace BuildingBlocks.Application.FileUploader;

public interface IFileUploader
{
    Task<UploaderResult> UploadAsync(string name, Stream stream, string contentType);

    void PushUpload(string name, Stream stream, string contentType);
}