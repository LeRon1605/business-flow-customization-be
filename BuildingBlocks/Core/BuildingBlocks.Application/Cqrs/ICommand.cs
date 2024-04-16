using MediatR;

namespace BuildingBlocks.Application.Cqrs;

public interface ICommand : ICqrsRequest, IRequest
{
    
}

public interface ICommand<out TResponse> : ICqrsRequest, IRequest<TResponse>
{
    
}