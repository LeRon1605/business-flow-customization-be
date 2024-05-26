using BuildingBlocks.Domain.Repositories;
using Hub.Domain.ConnectionAggregate.Entities;

namespace Hub.Domain.ConnectionAggregate.Repositories;

public interface IUserConnectionRepository : IRepository<UserConnection, int>
{
    
}