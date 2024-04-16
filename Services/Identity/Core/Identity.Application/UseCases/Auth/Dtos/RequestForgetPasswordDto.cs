using System.ComponentModel.DataAnnotations;

namespace Identity.Application.UseCases.Auth.Dtos;

public class RequestForgetPasswordDto
{
    [Required] 
    public string UserNameOrEmail { get; set; } = null!;

    [Required] 
    public string CallBackUrl { get; set; } = null!;
}