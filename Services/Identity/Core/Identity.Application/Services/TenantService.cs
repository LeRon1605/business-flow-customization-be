using Application.Dtos;
using AutoMapper;
using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Identity.Dtos;
using Identity.Application.Services.Interfaces;
using Identity.Domain.TenantAggregate;
using Identity.Domain.TenantAggregate.Entities;
using Identity.Domain.TenantAggregate.Exceptions;
using Identity.Domain.UserAggregate;
using Identity.Domain.UserAggregate.Exceptions;

namespace Identity.Application.Services;

public class TenantService : ITenantService
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly ITenantRepository _tenantRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public TenantService(IMapper mapper
        , IUserRepository userRepository
        , ITenantRepository tenantRepository
        , IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _tenantRepository = tenantRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IdentityResult<TenantDto>> CreateAsync(string name, string avatarUrl)
    {
        var tenant = new Tenant(name, avatarUrl);
        
        await _tenantRepository.InsertAsync(tenant);
        await _unitOfWork.CommitAsync();

        return IdentityResult<TenantDto>.Success(_mapper.Map<TenantDto>(tenant));
    }

    public async Task UpdateAsync(int tenantId, string name, string avatarUrl)
    {
        var tenant = await _tenantRepository.FindByIdAsync(tenantId);
        if (tenant == null)
        {
            throw new TenantNotFoundException(tenantId);
        }

        tenant.Update(name, avatarUrl);
        _tenantRepository.Update(tenant);
        await _unitOfWork.CommitAsync();
    }

    public async Task DeleteAsync(int tenantId)
    {
        var tenant = await _tenantRepository.FindByIdAsync(tenantId);
        if (tenant == null)
        {
            throw new TenantNotFoundException(tenantId);
        }
        
        _tenantRepository.Delete(tenant);
        await _unitOfWork.CommitAsync();
    }

    public async Task AddUserToTenantAsync(string userId, bool isOwner, int tenantId)
    {
        var user = await _userRepository.FindByIdAsync(userId);
        if (user == null)
        {
            throw new UserNotFoundException(userId);
        }

        if (!await _tenantRepository.IsExistingAsync(tenantId))
        {
            throw new TenantNotFoundException(tenantId);
        }
        
        user.AddToTenant(tenantId, isOwner);
        _userRepository.Update(user);
        await _unitOfWork.CommitAsync();
    }

    public async Task UpdateUserInTenantAsync(string userId, int tenantId, string fullname, string avatarUrl)
    {
        var user = await _userRepository.FindByIdAsync(userId);
        if (user == null)
        {
            throw new UserNotFoundException(userId);
        }
        
        user.UpdateProfileInTenant(fullname, avatarUrl, tenantId);
        _userRepository.Update(user);
        await _unitOfWork.CommitAsync();
    }

    public async Task RemoveUserFromTenantAsync(string userId, int tenantId)
    {
        var user = await _userRepository.FindByIdAsync(userId);
        if (user == null)
        {
            throw new UserNotFoundException(userId);
        }
        
        user.RemoveFromTenant(tenantId);
        _userRepository.Update(user);
        await _unitOfWork.CommitAsync();
    }
}