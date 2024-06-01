using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Identity;
using BuildingBlocks.Domain.Exceptions.Resources;
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
    private readonly ICurrentUser _currentUser;
    
    public CreateAccountTenantInvitationCommandHandler(IIdentityService identityService
        , ITenantRepository tenantRepository
        , IUnitOfWork unitOfWork
        , ICurrentUser currentUser)
    {
        _identityService = identityService;
        _tenantRepository = tenantRepository;
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
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
            
            var result = await _identityService.CreateUserAsync(request.FullName
                , invitation.Email
                , AppUser.DefaultAvatar
                , invitation.Email
                , request.Password);
            if (!result.IsSuccess)
            {
                throw new ResourceInvalidOperationException(result.Error!, result.ErrorCode!);
            }

            _currentUser.IsAuthenticated = true;
            _currentUser.Id = result.Data!.Id;
            _currentUser.TenantId = invitation.TenantId;
            
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