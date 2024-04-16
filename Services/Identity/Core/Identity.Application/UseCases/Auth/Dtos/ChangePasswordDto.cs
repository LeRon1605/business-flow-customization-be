using System.ComponentModel.DataAnnotations;

namespace Identity.Application.UseCases.Auth.Dtos;

public class ChangePasswordDto
{
    [Required]
    [MinLength(6)]
    public string CurrentPassword { get; set; } = null!;
    
    [Required] 
    [MinLength(6)] 
    public string NewPassword { get; set; } = null!;
}