using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Identity;
using Identity.Application.Services.Dtos;
using Identity.Application.Services.Interfaces;
using Identity.Application.UseCases.Auth.Dtos;
using Identity.Domain.UserAggregate;
using Identity.Domain.UserAggregate.Exceptions;

namespace Identity.Application.UseCases.Auth.Commands;

public class ExchangeTenantAccessTokenCommandHandler : ICommandHandler<ExchangeTenantAccessTokenCommand, AuthCredentialDto>
{
    private readonly JwtSetting _jwtSetting;
    private readonly ICurrentUser _currentUser;
    private readonly IUserRepository _userRepository;
    private readonly ITokenProvider _tokenProvider;
    private readonly IUnitOfWork _unitOfWork;
    
    public ExchangeTenantAccessTokenCommandHandler(JwtSetting jwtSetting
        , ICurrentUser currentUser
        , IUserRepository userRepository
        , ITokenProvider tokenProvider
        , IUnitOfWork unitOfWork)
    {
        _jwtSetting = jwtSetting;
        _currentUser = currentUser;
        _userRepository = userRepository;
        _tokenProvider = tokenProvider;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<AuthCredentialDto> Handle(ExchangeTenantAccessTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindByIdAsync(_currentUser.Id);
        if (user == null)
        {
            throw new UserNotFoundException(_currentUser.Id);
        }
        
        var accessToken = await _tokenProvider.GenerateAccessTokenAsync(user, request.TenantId);
        var credential = new AuthCredentialDto()
        {
            AccessToken = accessToken.Value,
            RefreshToken = _tokenProvider.GenerateRefreshToken()
        };
            
        user.AddRefreshToken(accessToken.Id, credential.RefreshToken, DateTime.UtcNow.AddDays(_jwtSetting.RefreshTokenExpiresInDay));
            
        _userRepository.Update(user);
        await _unitOfWork.CommitAsync();

        return credential;
    }
}