using BuildingBlocks.Application.Cqrs;
using Submission.Application.UseCases.Dtos;

namespace Submission.Application.UseCases.Forms.Queries;

public class GetBusinessFlowBlockFormQuery : IQuery<FormDto>
{
    public Guid BusinessFlowBlockId { get; set; }
    
    public GetBusinessFlowBlockFormQuery(Guid businessFlowBlockId)
    {
        BusinessFlowBlockId = businessFlowBlockId;
    }
}