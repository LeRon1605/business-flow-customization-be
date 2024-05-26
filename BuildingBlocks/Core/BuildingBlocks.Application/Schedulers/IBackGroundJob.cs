using MediatR;

namespace BuildingBlocks.Application.Schedulers;

public interface IBackGroundJob : INotification
{
    public string? UserId { get; set; }
    public int? TenantId { get; set; }
}