using MediatR;

namespace BuildingBlocks.Application.Cqrs;

public interface IQuery<out TResponse> : ICqrsRequest, IRequest<TResponse>
{
    
}