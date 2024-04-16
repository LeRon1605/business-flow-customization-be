using BuildingBlocks.Kernel.Services;
using Microsoft.AspNetCore.Http;

namespace BuildingBlocks.Presentation.ExceptionHandlers;

public interface IExceptionHandler : IScopedService
{
    Task HandleAsync(HttpContext context, Exception exception);
}