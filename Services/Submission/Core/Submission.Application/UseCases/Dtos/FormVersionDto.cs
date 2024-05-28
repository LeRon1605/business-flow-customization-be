﻿using System.Linq.Expressions;
using BuildingBlocks.Domain.Repositories;
using Submission.Domain.FormAggregate.Entities;

namespace Submission.Application.UseCases.Dtos;

public class FormVersionDto : IProjection<FormVersion, FormVersionDto>
{
    public int Id { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public Expression<Func<FormVersion, FormVersionDto>> GetProject()
    {
        return x => new FormVersionDto()
        {
            Id = x.Id,
            CreatedAt = x.Created
        };
    }
}