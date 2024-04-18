using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Helpers;
using BuildingBlocks.Application.MailSender;
using BuildingBlocks.Domain.Exceptions.Resources;
using BuildingBlocks.EventBus.Abstracts;
using Identity.Application.Services.Dtos;
using Identity.Application.UseCases.Auth.Dtos;
using Identity.Domain.UserAggregate;
using Identity.Domain.UserAggregate.Entities;
using Identity.Domain.UserAggregate.Exceptions;
using IntegrationEvents.Hub;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;

namespace Identity.Application.UseCases.Auth.Commands;

public class RequestForgetPasswordTokenCommandHandler : ICommandHandler<RequestForgetPasswordTokenCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IEventPublisher _eventPublisher;
    private readonly ForgetPasswordSetting _forgetPasswordSetting;
    private readonly UserManager<ApplicationUser> _userManager;
    
    public RequestForgetPasswordTokenCommandHandler(UserManager<ApplicationUser> userManager
        , ForgetPasswordSetting forgetPasswordSetting
        , IEventPublisher eventPublisher
        , IUserRepository userRepository)
    {
        _userRepository = userRepository;
        _userManager = userManager;
        _forgetPasswordSetting = forgetPasswordSetting;
        _eventPublisher = eventPublisher;
    }
    
    public async Task Handle(RequestForgetPasswordTokenCommand request, CancellationToken cancellationToken)
    {
        if (!_forgetPasswordSetting.AllowedClients.Contains(request.CallBackUrl))
        {
            throw new ResourceInvalidOperationException("CallBackUrl is not allowed!");
        }
        
        var user = await _userRepository.FindByUserNameOrEmailAsync(request.UserNameOrEmail, request.UserNameOrEmail);
        if (user == null)
        {
            throw new UserNameOrEmailNotFoundException(request.UserNameOrEmail);
        }

        var token = new ForgetPasswordTokenDto()
        {
            Value = await _userManager.GeneratePasswordResetTokenAsync(user),
            UserId = user.Id
        };
        
        var callBackUrlWithToken = QueryHelpers.AddQueryString(request.CallBackUrl, new Dictionary<string, string>()
        {
            {"token", Encryptor.Base64Encode<ForgetPasswordTokenDto>(token) }
        });

        await _eventPublisher.Publish(new EmailSenderIntegrationEvent("Đổi mật khẩu"
            ,user.Email!
            , "RequestForgetPassword"
            , new
            {
                CallBackUrl = callBackUrlWithToken
            }), cancellationToken);
    }
}