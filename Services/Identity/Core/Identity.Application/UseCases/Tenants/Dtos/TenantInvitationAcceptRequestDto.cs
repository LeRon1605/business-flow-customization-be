using System.ComponentModel.DataAnnotations;

namespace Identity.Application.UseCases.Tenants.Dtos;

public class TenantInvitationAcceptRequestDto
{
    [Required] 
    public string Token { get; set; } = null!;
}