using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Helpers;
using BuildingBlocks.Domain.Exceptions.Resources;
using Identity.Application.UseCases.Auth.Dtos;
using Identity.Domain.UserAggregate;
using Identity.Domain.UserAggregate.Entities;
using Identity.Domain.UserAggregate.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Identity.Application.UseCases.Auth.Commands;

public class ResetPasswordByTokenCommandHandler : ICommandHandler<ResetPasswordByTokenCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    
    public ResetPasswordByTokenCommandHandler(UserManager<ApplicationUser> userManager
        , IUserRepository userRepository)
    {
        _userRepository = userRepository;
        _userManager = userManager;
    }
    
    public async Task Handle(ResetPasswordByTokenCommand request, CancellationToken cancellationToken)
    {
        var deserializedToken = DeserializeToken(request.Token);
        
        var user = await _userRepository.FindByIdAsync(deserializedToken.UserId);
        if (user == null)
        {
            throw new InvalidForgetPasswordTokenException();
        }
        
        var result = await _userManager.ResetPasswordAsync(user, deserializedToken.Value, request.Password);
        if (!result.Succeeded)
        {
            var error = result.Errors.First();
            throw new ResourceInvalidOperationException(error.Description, error.Code);
        }
    }
    
    private ForgetPasswordTokenDto DeserializeToken(string token)
    {
        try
        {
            var deserializedToken = Encryptor.Base64Decode<ForgetPasswordTokenDto>(token);
            if (deserializedToken == null)
                throw new InvalidForgetPasswordTokenException();
            
            return deserializedToken;
        }
        catch 
        {
            throw new InvalidForgetPasswordTokenException();
        }
    }
}