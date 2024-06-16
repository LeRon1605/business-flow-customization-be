using BuildingBlocks.Domain.Services;
using Submission.Domain.FormAggregate.Entities;
using Submission.Domain.FormAggregate.Models;

namespace Submission.Domain.FormAggregate.DomainServices;

public interface IFormDomainService : IDomainService
{
    Task<Form> CreateAsync(int spaceId, FormModel formModel);

    Task<FormVersion> UpdateAsync(Form form, FormModel formModel);
}