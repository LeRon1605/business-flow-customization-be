using System.ComponentModel.DataAnnotations;

namespace Identity.Application.UseCases.Auth.Dtos;

public class ExchangeTenantAccessTokenDto
{
    [Required]
    public int TenantId { get; set; }
}