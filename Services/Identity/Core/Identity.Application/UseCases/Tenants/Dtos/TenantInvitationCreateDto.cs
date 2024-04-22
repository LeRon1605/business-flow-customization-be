using System.ComponentModel.DataAnnotations;

namespace Identity.Application.UseCases.Tenants.Dtos;

public class TenantInvitationCreateDto
{
    [Required] 
    [EmailAddress]
    public string Email { get; set; } = null!;
    
    [Required]
    public string RoleId { get; set; } = null!;
}