﻿namespace Application.Dtos;

public class PermissionDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
}