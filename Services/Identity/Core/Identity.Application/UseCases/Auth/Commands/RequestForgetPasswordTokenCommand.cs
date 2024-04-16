using BuildingBlocks.Application.Cqrs;

namespace Identity.Application.UseCases.Auth.Commands;

public class RequestForgetPasswordTokenCommand : ICommand
{
    public string UserNameOrEmail { get; set; }
    public string CallBackUrl { get; set; }

    public RequestForgetPasswordTokenCommand(string userNameOrEmail, string callBackUrl)
    {
        UserNameOrEmail = userNameOrEmail;
        CallBackUrl = callBackUrl;
    }
}