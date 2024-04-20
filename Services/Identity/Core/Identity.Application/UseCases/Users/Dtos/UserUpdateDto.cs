using System.ComponentModel.DataAnnotations;

namespace Identity.Application.UseCases.Users.Dtos;

public class UserUpdateDto
{
    [Required]
    public string FullName { get; set; } = null!;
    
    [Required]
    public string AvatarUrl { get; set; } = null!;
}