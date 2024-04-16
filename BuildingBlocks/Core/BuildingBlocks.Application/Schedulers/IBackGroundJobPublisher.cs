namespace BuildingBlocks.Application.Schedulers;

public interface IBackGroundJobPublisher
{
    Task Publish(IBackGroundJob job);
}