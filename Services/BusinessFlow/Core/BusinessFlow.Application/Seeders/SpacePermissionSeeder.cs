using Application.Seeders;
using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Repositories;
using BusinessFlow.Domain.SpaceAggregate.Entities;

namespace BusinessFlow.Application.Seeders;

public class SpacePermissionSeeder : DataSeeder
{
    public override int Id => 2;
    private readonly IRepository<SpacePermission, int> _spacePermissionRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public SpacePermissionSeeder(IServiceProvider serviceProvider
        , IRepository<SpacePermission, int> spacePermissionRepository
        , IUnitOfWork unitOfWork) : base(serviceProvider)
    {
        _spacePermissionRepository = spacePermissionRepository;
        _unitOfWork = unitOfWork;
    }
    public override async Task SeedAsync()
    {
        if (await _spacePermissionRepository.AnyAsync())
        {
            return;
        }
        
        var spacePermissions = GetSpacePermissions();
        await _spacePermissionRepository.InsertRangeAsync(spacePermissions);
        await _unitOfWork.CommitAsync();
    }
    
    private List<SpacePermission> GetSpacePermissions()
    {
        return new List<SpacePermission>
        {
            new SpacePermission(1, "Record.View"),
            new SpacePermission(1, "Record.Comment"),
            new SpacePermission(1, "Record.Edit"),
            new SpacePermission(1, "Record.Delete"),
            new SpacePermission(1, "Form.Share"),
            new SpacePermission(1, "Form.Edit"),
            new SpacePermission(1, "Form.View"),
            new SpacePermission(1, "Flow.Edit"),
            new SpacePermission(1, "Flow.View"),
            new SpacePermission(1, "Space.Edit"),
            new SpacePermission(1, "Space.Delete"),
            new SpacePermission(1, "Space.AddMember"),
            new SpacePermission(1, "Space.RemoveMember"),
            new SpacePermission(2, "Record.View"),
            new SpacePermission(2, "Record.Comment"),
            new SpacePermission(2, "Record.Edit"),
            new SpacePermission(2, "Record.Delete"),
            new SpacePermission(2, "Form.Share"),
            new SpacePermission(2, "Form.View"),
            new SpacePermission(2, "Flow.View"),
            new SpacePermission(3, "Record.View"),
            new SpacePermission(3, "Record.Comment"),
        };
    }
}