﻿using System.ComponentModel.DataAnnotations;

namespace Identity.Application.UseCases.Auth.Dtos;

public class SignInDto
{
    [Required] 
    public string UserNameOrEmail { get; set; } = null!;

    [Required] 
    public string Password { get; set; } = null!;
}