namespace BuildingBlocks.Application.Dtos;

public class ErrorResponseDto
{
    public string Code { get; set; } = null!;
    public string? Message { get; set; } = null!;
}