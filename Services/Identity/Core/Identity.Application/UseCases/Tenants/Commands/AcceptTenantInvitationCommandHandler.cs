using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Identity;
using BuildingBlocks.Domain.Repositories;
using Identity.Application.UseCases.Tenants.Dtos;
using Identity.Application.UseCases.Tenants.Dtos.Responses;
using Identity.Domain.TenantAggregate;
using Identity.Domain.TenantAggregate.Exceptions;
using Identity.Domain.UserAggregate.Entities;
using Identity.Domain.UserAggregate.Specifications;

namespace Identity.Application.UseCases.Tenants.Commands;

public class AcceptTenantInvitationCommandHandler : ICommandHandler<AcceptTenantInvitationCommand, AcceptTenantInvitationResponseDto>
{
    private readonly IBasicReadOnlyRepository<ApplicationUser, string> _userRepository;
    private readonly ITenantRepository _tenantRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUser _currentUser;
    
    public AcceptTenantInvitationCommandHandler(IBasicReadOnlyRepository<ApplicationUser, string> userRepository
        , ITenantRepository tenantRepository
        , IUnitOfWork unitOfWork
        , ICurrentUser currentUser)
    {
        _userRepository = userRepository;
        _tenantRepository = tenantRepository;
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
    }
    
    public async Task<AcceptTenantInvitationResponseDto> Handle(AcceptTenantInvitationCommand request, CancellationToken cancellationToken)
    {
        var tenant = await _tenantRepository.FindByInvitationTokenAsync(request.Token);
        if (tenant == null)
        {
            throw new TenantInvitationNotFoundException(request.Token);    
        }

        var invitation = tenant.Invitations.First(x => x.Token == request.Token);
        var user = await _userRepository.FindAsync(new UserByEmailSpecification(invitation.Email));
        if (user == null)
        {
            return new AcceptTenantInvitationResponseDto()
            {
                IsUserExisted = false
            };    
        }
        
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            _currentUser.IsAuthenticated = true;
            _currentUser.Id = user.Id;
            _currentUser.TenantId = tenant.Id;
            
            tenant.AcceptInvitation(request.Token);
            await _unitOfWork.CommitTransactionAsync();
        }
        catch 
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
            
        return new AcceptTenantInvitationResponseDto()
        {
            IsUserExisted = true
        };

    }
}