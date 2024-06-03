using System.Linq.Expressions;
using Application.Dtos.Notifications.Requests;
using Application.Dtos.Notifications.Responses;
using BuildingBlocks.Domain.Repositories;
using BusinessFlow.Domain.SubmissionExecutionAggregate.Entities;
using Newtonsoft.Json;

namespace BusinessFlow.Application.UseCases.Notifications.Dtos;

public class PersonInChargeQueryDto : BusinessFlowNotificationDataDto, IProjection<SubmissionExecution, PersonInChargeQueryDto>
{
    public Expression<Func<SubmissionExecution, PersonInChargeQueryDto>> GetProject()
    {
        return x => new PersonInChargeQueryDto
        {
            Id = JsonConvert.SerializeObject(x.PersonInCharges.Select(p => p.UserId)),
           Type = BusinessFlowNotificationDataType.ExecutionPersonInCharge
        };
    }
}