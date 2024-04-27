using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Data;
using Domain.Identities;
using Identity.Application.Services.Interfaces;
using Identity.Domain.TenantAggregate;
using Identity.Domain.TenantAggregate.Exceptions;

namespace Identity.Application.UseCases.Tenants.Commands;

public class CreateAccountTenantInvitationCommandHandler : ICommandHandler<CreateAccountTenantInvitationCommand>
{
    private readonly IIdentityService _identityService;
    private readonly ITenantRepository _tenantRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public CreateAccountTenantInvitationCommandHandler(IIdentityService identityService
        , ITenantRepository tenantRepository
        , IUnitOfWork unitOfWork)
    {
        _identityService = identityService;
        _tenantRepository = tenantRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(CreateAccountTenantInvitationCommand request, CancellationToken cancellationToken)
    {
        var tenant = await _tenantRepository.FindByInvitationTokenAsync(request.Token);
        if (tenant == null)
        {
            throw new TenantInvitationNotFoundException(request.Token);    
        }
        
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var invitation = tenant.Invitations.First(x => x.Token == request.Token);
            
            await _identityService.CreateUserAsync(request.FullName
                , invitation.Email
                , AppUser.DefaultAvatar
                , invitation.Email
                , request.Password);
            
            tenant.AcceptInvitation(request.Token);
            
            await _unitOfWork.CommitTransactionAsync();
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}