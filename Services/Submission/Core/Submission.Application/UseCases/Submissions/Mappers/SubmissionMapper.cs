using Application.Dtos.Notifications.Responses;
using BuildingBlocks.Application.Mappers;
using Submission.Application.UseCases.Submissions.Dtos;
using Submission.Domain.SubmissionAggregate.Models;

namespace Submission.Application.UseCases.Submissions.Mappers;

public class SubmissionMapper : MappingProfile
{
    public SubmissionMapper()
    {
        CreateMap<SubmitFormDto, SubmissionModel>();
        CreateMap<SubmissionFieldDto, SubmissionFieldModel>();
        CreateMap<BasicSubmissionDto, SubmissionNotificationDataDto>();
    }
}