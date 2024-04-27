using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Repositories;
using Domain.Identities;
using Identity.Application.Services.Interfaces;
using Identity.Domain.TenantAggregate;
using Identity.Domain.TenantAggregate.Exceptions;
using Identity.Domain.UserAggregate.Entities;
using Identity.Domain.UserAggregate.Specifications;

namespace Identity.Application.UseCases.Tenants.Commands;

public class InitAccountTenantInvitationCommandHandler : ICommandHandler<InitAccountTenantInvitationCommand>
{
    private readonly IBasicReadOnlyRepository<ApplicationUser, string> _userRepository;
    private readonly IIdentityService _identityService;
    private readonly ITenantRepository _tenantRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public InitAccountTenantInvitationCommandHandler(IBasicReadOnlyRepository<ApplicationUser, string> userRepository
        , IIdentityService identityService
        , ITenantRepository tenantRepository
        , IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _identityService = identityService;
        _tenantRepository = tenantRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(InitAccountTenantInvitationCommand request, CancellationToken cancellationToken)
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