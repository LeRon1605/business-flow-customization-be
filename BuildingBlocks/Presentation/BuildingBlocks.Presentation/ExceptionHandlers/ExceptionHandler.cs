using System.ComponentModel.DataAnnotations;
using BuildingBlocks.Application.Dtos;
using BuildingBlocks.Domain.Exceptions;
using BuildingBlocks.Domain.Exceptions.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Presentation.ExceptionHandlers;

public class ExceptionHandler : IExceptionHandler
{
    private readonly ILogger<ExceptionHandler> _logger;
    private readonly IHostEnvironment _env;

    public ExceptionHandler(ILogger<ExceptionHandler> logger, IHostEnvironment env)
    {
        _logger = logger;
        _env = env;
    }

    public async Task HandleAsync(HttpContext context, Exception exception)
    {
        LogException(context, exception);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = GetResponseStatusCode(exception);

        var errorResponse = GetErrorResponse(exception);
        await context.Response.WriteAsJsonAsync(errorResponse);
    }

    private void LogException(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, "Exception occurred on request [{method}] {path}", context.Request.Method, context.Request.Path);
    }

    private ErrorResponseDto GetErrorResponse(Exception exception)
    {
        return new ErrorResponseDto()
        {
            Code = exception switch
            {
                CoreException coreException => coreException.ErrorCode,
                ValidationException validationException => "ValidationError",
                _ => ErrorCodes.SystemError
            },
            Message = exception switch
            {
                CoreException coreException => coreException.Message,
                ValidationException validationException => validationException.ValidationResult.ErrorMessage,
                _ => _env.IsDevelopment() ? exception.Message : "Something went wrong, please try again!"
            }
        };
    }

    private int GetResponseStatusCode(Exception exception)
    {
        return exception switch
        {
            ResourceNotFoundException => StatusCodes.Status404NotFound,
            ResourceAccessDeniedException => StatusCodes.Status403Forbidden,
            ResourceAlreadyExistException => StatusCodes.Status409Conflict,
            ResourceInvalidOperationException => StatusCodes.Status400BadRequest,
            ResourceInvalidDataException => StatusCodes.Status400BadRequest,
            ResourceUnauthorizedAccessException => StatusCodes.Status401Unauthorized,
            ValidationException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };
    }
}