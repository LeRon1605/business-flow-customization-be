using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.FileUploader;
using BuildingBlocks.Domain.Exceptions.Resources;

namespace Hub.Application.UseCases;

public class UploadFileCommandHandler : ICommandHandler<UploadFileCommand, string>
{
    private readonly IFileUploader _fileUploader;
    
    public UploadFileCommandHandler(IFileUploader fileUploader)
    {
        _fileUploader = fileUploader;
    }
    
    public async Task<string> Handle(UploadFileCommand request, CancellationToken cancellationToken)
    {
        var result = await _fileUploader.UploadAsync(request.File.FileName, request.File.OpenReadStream());
        if (!result.IsSuccess)
        {
            throw new ResourceInvalidOperationException(result.ErrorMessage!, "UploadImageFailed");
        }

        return result.Url!;
    }
}