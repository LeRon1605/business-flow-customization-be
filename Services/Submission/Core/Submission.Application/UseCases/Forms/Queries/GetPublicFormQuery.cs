using BuildingBlocks.Application.Cqrs;
using Submission.Application.UseCases.Dtos;

namespace Submission.Application.UseCases.Forms.Queries;

public class GetPublicFormQuery : IQuery<FormDto>
{
    public string Token { get; set; }
    
    public GetPublicFormQuery(string token)
    {
        Token = token;
    }
}