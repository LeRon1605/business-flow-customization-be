namespace BuildingBlocks.Domain.Models.Interfaces;

public interface IHasTenant
{
    int TenantId { get; set; }
}