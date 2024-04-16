using System.Security.Claims;
using BuildingBlocks.Kernel.Services;
using Identity.Domain.UserAggregate.Entities;

namespace Identity.Application.Services.Interfaces;

public interface IClaimGenerator : IScopedService
{
    Task<List<Claim>> GenerateAsync(ApplicationUser user, int? tenantId = null);
}