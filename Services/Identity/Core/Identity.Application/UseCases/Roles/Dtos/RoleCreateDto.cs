using System.ComponentModel.DataAnnotations;

namespace Identity.Application.UseCases.Roles.Dtos;

public class RoleCreateDto
{
    [Required] 
    public string Name { get; set; } = null!;
}