using Application.Seeders;
using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Repositories;
using BusinessFlow.Domain.SpaceAggregate.Entities;

namespace BusinessFlow.Application.Seeders;

public class SpaceRoleSeeder : DataSeeder
{
    public override int Id => 1;
    private readonly IRepository<SpaceRole, int> _spaceRoleRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public SpaceRoleSeeder(IServiceProvider serviceProvider
        , IRepository<SpaceRole, int> spaceRoleRepository
        , IUnitOfWork unitOfWork) : base(serviceProvider)
    {
        _spaceRoleRepository = spaceRoleRepository;
        _unitOfWork = unitOfWork;
    }
    
    public override async Task SeedAsync()
    {
        if (await _spaceRoleRepository.AnyAsync())
        {
            return;
        }
        
        var spaceRoles = GetSpaceRoles();
        await _spaceRoleRepository.InsertRangeAsync(spaceRoles);
        await _unitOfWork.CommitAsync();
    }
    
    private List<SpaceRole> GetSpaceRoles()
    {
        return new List<SpaceRole>
        {
            new SpaceRole("Trưởng dự án"),
            new SpaceRole("Người chỉnh sửa"),
            new SpaceRole("Người xem")
        };
    }
}