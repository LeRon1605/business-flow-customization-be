﻿using BuildingBlocks.Application.Cqrs;
using Microsoft.AspNetCore.Http;

namespace Hub.Application.UseCases;

public class UploadFileCommand : ICommand<string>
{
    public IFormFile File { get; set; }
    
    public UploadFileCommand(IFormFile file)
    {
        File = file;
    }
}