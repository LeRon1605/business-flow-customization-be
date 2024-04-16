using System.ComponentModel.DataAnnotations;

namespace Identity.Application.UseCases.Roles.Dtos;

public class RoleUpdateDto
{
    [Required] 
    public string Name { get; set; } = null!;
}