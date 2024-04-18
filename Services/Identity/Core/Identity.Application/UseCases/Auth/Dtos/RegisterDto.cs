using System.ComponentModel.DataAnnotations;

namespace Identity.Application.UseCases.Auth.Dtos;

public class RegisterDto
{
    [Required] 
    [EmailAddress] 
    public string Email { get; set; } = null!;

    [Required] 
    public string FullName { get; set; } = null!;

    [Required]
    public string TenantName { get; set; } = null!;
    
    [Required]
    public string Password { get; set; } = null!;
}