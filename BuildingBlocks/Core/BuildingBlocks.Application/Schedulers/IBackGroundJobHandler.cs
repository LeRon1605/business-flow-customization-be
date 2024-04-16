using MediatR;

namespace BuildingBlocks.Application.Schedulers;

public interface IBackGroundJobHandler<in T> : INotificationHandler<T> where T : IBackGroundJob
{
    
}