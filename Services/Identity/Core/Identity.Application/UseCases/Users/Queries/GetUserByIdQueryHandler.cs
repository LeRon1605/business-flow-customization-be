using Application.Dtos;
using Application.Dtos.Submissions.Identity;
using AutoMapper;
using BuildingBlocks.Application.Cqrs;
using Identity.Application.UseCases.Users.Dtos;
using Identity.Domain.TenantAggregate;
using Identity.Domain.UserAggregate;
using Identity.Domain.UserAggregate.Exceptions;

namespace Identity.Application.UseCases.Users.Queries;

public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserDetailDto>
{
    private readonly IUserRepository _userRepository;
    private readonly ITenantRepository _tenantRepository;
    private readonly IMapper _mapper;
    
    public GetUserByIdQueryHandler(IUserRepository userRepository
        , ITenantRepository tenantRepository
        , IMapper mapper)
    {
        _userRepository = userRepository;
        _tenantRepository = tenantRepository;
        _mapper = mapper;
    }

    public async Task<UserDetailDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindByIdAsync(request.Id);
        if (user == null)
        {
            throw new UserNotFoundException(request.Id);
        }
        
        var tenants = await _tenantRepository.FindByUserAsync(user.Id);
        
        var userInfo = new UserDetailDto()
        {
            Id = user.Id,
            UserName = user.UserName!,
            Email = user.Email!,
            Tenants = _mapper.Map<IEnumerable<TenantDto>>(tenants)
        };
        
        return userInfo;
    }
}