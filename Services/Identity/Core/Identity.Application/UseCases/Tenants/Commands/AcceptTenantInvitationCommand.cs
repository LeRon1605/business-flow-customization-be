using BuildingBlocks.Application.Cqrs;
using Identity.Application.UseCases.Tenants.Dtos;

namespace Identity.Application.UseCases.Tenants.Commands;

public class AcceptTenantInvitationCommand : ICommand<TenantInvitationAcceptResponseDto>
{
    public string Token { get; set; }
    
    public AcceptTenantInvitationCommand(string token)
    {
        Token = token;
    }
}