using System.ComponentModel.DataAnnotations;

namespace Identity.Application.UseCases.Tenants.Dtos;

public class TenantUpdateDto
{
    [Required] 
    public string Name { get; set; } = null!;

    [Required]
    public string AvatarUrl { get; set; } = null!;
}