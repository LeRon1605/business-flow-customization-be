using AutoMapper;
using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Dtos;
using BuildingBlocks.Domain.Specifications.Interfaces;
using Identity.Application.UseCases.Users.Dtos;
using Identity.Domain.UserAggregate;
using Identity.Domain.UserAggregate.Entities;
using Identity.Domain.UserAggregate.Specifications;

namespace Identity.Application.UseCases.Users.Queries;

public class GetAllUsersQueryHandler: IQueryHandler<GetAllUsersQuery, PagedResultDto<UserDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    
    public GetAllUsersQueryHandler(IUserRepository userRepository
        , IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task<PagedResultDto<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var specification = GetSpecification(request);
        var users = await _userRepository.GetPagedListAsync(specification);
        var total = await _userRepository.GetCountAsync(specification);
        return new PagedResultDto<UserDto>(
            total,
            request.Size,
            _mapper.Map<IEnumerable<UserDto>>(users));
    }
    
    private IPagingAndSortingSpecification<ApplicationUser, string> GetSpecification(GetAllUsersQuery request)
    {
        var specification = new FilterApplicationUserSpecification(request.Page, request.Size, request.Sorting, request.Search);
        return specification;
    }
}