using BuildingBlocks.Application.Cqrs;
using Identity.Application.UseCases.Tenants.Dtos;
using Identity.Application.UseCases.Tenants.Dtos.Responses;

namespace Identity.Application.UseCases.Tenants.Commands;

public class AcceptTenantInvitationCommand : ICommand<AcceptTenantInvitationResponseDto>
{
    public string Token { get; set; }
    
    public AcceptTenantInvitationCommand(string token)
    {
        Token = token;
    }
}