using BuildingBlocks.Application.Cqrs;
using Identity.Application.UseCases.Auth.Dtos;

namespace Identity.Application.UseCases.Auth.Commands;

public class ExchangeTenantAccessTokenCommand : ICommand<AuthCredentialDto>
{
    public int TenantId { get; set; }
    
    public ExchangeTenantAccessTokenCommand(int tenantId)
    {
        TenantId = tenantId;
    }
}