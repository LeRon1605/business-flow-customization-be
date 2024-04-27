using System.ComponentModel.DataAnnotations;

namespace Identity.Application.UseCases.Tenants.Dtos;

public class InitAccountTenantInvitationDto
{
    [Required]
    public string FullName { get; set; } = null!;
    
    [Required]
    public string Token { get; set; } = null!;
    
    [Required]
    public string Password { get; set; } = null!;
}