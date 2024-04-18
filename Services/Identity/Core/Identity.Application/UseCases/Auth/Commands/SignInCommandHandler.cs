using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Data;
using Identity.Application.Services.Dtos;
using Identity.Application.Services.Interfaces;
using Identity.Application.UseCases.Auth.Dtos;
using Identity.Domain.UserAggregate;
using Identity.Domain.UserAggregate.Entities;
using Identity.Domain.UserAggregate.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Identity.Application.UseCases.Auth.Commands;

public class SignInCommandHandler : ICommandHandler<SignInCommand, AuthCredentialDto>
{
    private readonly JwtSetting _jwtSetting;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITokenProvider _tokenProvider;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SignInCommandHandler(JwtSetting jwtSetting
        , SignInManager<ApplicationUser> signInManager
        , ITokenProvider tokenProvider
        , IUserRepository userRepository
        , IUnitOfWork unitOfWork)
    {
        _jwtSetting = jwtSetting;
        _signInManager = signInManager;
        _tokenProvider = tokenProvider;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<AuthCredentialDto> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindByUserNameOrEmailAsync(request.UserNameOrEmail, request.UserNameOrEmail);
        if (user == null)
        {
            throw new UserNameOrEmailNotFoundException(request.UserNameOrEmail);
        }

        var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, true);
        if (result.Succeeded)
        {
            var accessToken = await _tokenProvider.GenerateAccessTokenAsync(user);
            var credential = new AuthCredentialDto()
            {
                AccessToken = accessToken.Value,
                RefreshToken = _tokenProvider.GenerateRefreshToken()
            };
            
            user.AddRefreshToken(accessToken.Id
                , credential.RefreshToken
                , DateTime.UtcNow.AddDays(_jwtSetting.RefreshTokenExpiresInDay));
            
            _userRepository.Update(user);
            await _unitOfWork.CommitAsync();

            return credential;
        }

        if (result.IsLockedOut)
        {
            throw new AccountLockedOutException();
        }

        throw new InvalidCredentialException();
    }
}