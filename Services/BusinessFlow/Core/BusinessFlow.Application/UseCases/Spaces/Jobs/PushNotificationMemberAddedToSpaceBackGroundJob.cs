using BuildingBlocks.Application.Schedulers;

namespace BusinessFlow.Application.UseCases.Spaces.Jobs;

public class PushNotificationMemberAddedToSpaceBackGroundJob : BackGroundJob
{
    public int SpaceId { get; set; }
    
    public string MemberId { get; set; }

    public PushNotificationMemberAddedToSpaceBackGroundJob(int spaceId, string memberId)
    {
        SpaceId = spaceId;
        MemberId = memberId;
    }
}