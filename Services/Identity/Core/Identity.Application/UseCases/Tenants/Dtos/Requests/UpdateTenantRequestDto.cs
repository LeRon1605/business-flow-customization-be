using System.ComponentModel.DataAnnotations;

namespace Identity.Application.UseCases.Tenants.Dtos.Requests;

public class UpdateTenantRequestDto
{
    [Required] 
    public string Name { get; set; } = null!;

    [Required]
    public string AvatarUrl { get; set; } = null!;
}