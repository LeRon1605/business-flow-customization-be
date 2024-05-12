﻿using Submission.Domain.FormAggregate.Entities;
using Submission.Domain.FormAggregate.Exceptions;
using Submission.Domain.FormAggregate.Models;
using Submission.Domain.FormAggregate.Repositories;
using Submission.Domain.FormAggregate.Specifications;

namespace Submission.Domain.FormAggregate.DomainServices;

public class FormDomainService : IFormDomainService
{
    private readonly IFormVersionRepository _formVersionRepository;
    private readonly IFormRepository _formRepository;
    
    public FormDomainService(IFormVersionRepository formVersionRepository, IFormRepository formRepository)
    {
        _formVersionRepository = formVersionRepository;
        _formRepository = formRepository;
    }
    
    public async Task<Form> CreateAsync(int spaceId, FormModel formModel)
    {
        var isExisted = await _formRepository.AnyAsync(new SpaceFormSpecification(spaceId));
        if (isExisted)
        {
            throw new SpaceFormAlreadyExistedException(spaceId);
        }
        
        var form = new Form(spaceId, formModel.Name, formModel.CoverImageUrl);
        form.AddVersion(new FormVersion(formModel.Elements));
        
        await _formRepository.InsertAsync(form);
        
        return form;
    }
}