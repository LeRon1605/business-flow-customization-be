using System.Security.Claims;
using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Identity;
using Domain;
using Domain.Claims;
using Identity.Application.Services.Dtos;
using Identity.Application.Services.Interfaces;
using Identity.Application.UseCases.Auth.Dtos;
using Identity.Domain.UserAggregate;
using Identity.Domain.UserAggregate.Exceptions;

namespace Identity.Application.UseCases.Auth.Commands;

public class RefreshTokenCommandHandler : ICommandHandler<RefreshTokenCommand, AuthCredentialDto>
{
    private readonly JwtSetting _jwtSetting;
    private readonly ITokenProvider _tokenProvider;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public RefreshTokenCommandHandler(JwtSetting jwtSetting
        , ITokenProvider tokenProvider
        , IUserRepository userRepository
        , IUnitOfWork unitOfWork)
    {
        _jwtSetting = jwtSetting;
        _tokenProvider = tokenProvider;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<AuthCredentialDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindByRefreshTokenAsync(request.RefreshToken);
        if (user == null)
        {
            throw new RefreshTokenNotFoundException(request.RefreshToken);
        }

        var accessTokenId = string.Empty;
        var claims = new List<Claim>();
        if (_tokenProvider.ValidateToken(request.AccessToken, ref accessTokenId, ref claims))
        {
            throw new AccessTokenStillValidException();
        }
        
        user.UseRefreshToken(accessTokenId, request.RefreshToken);

        var tenantId = claims.FirstOrDefault(x => x.Type == AppClaim.TenantId)?.Value;
        var accessToken = await _tokenProvider.GenerateAccessTokenAsync(user, int.Parse(tenantId));
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