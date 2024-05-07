using BuildingBlocks.Application.Cqrs;
using BusinessFlow.Application.UseCases.BusinessFlows.Dtos;

namespace BusinessFlow.Application.UseCases.BusinessFlows.Queries;

public class GetSpaceBusinessFlowVersionQuery : IQuery<IList<BusinessFlowVersionDto>>
{
    public int Id { get; set; }
    
    public GetSpaceBusinessFlowVersionQuery(int id)
    {
        Id = id;
    }
}