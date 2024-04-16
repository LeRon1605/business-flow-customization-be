﻿namespace Application.Dtos;

public class TenantDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? AvatarUrl { get; set; }
}