using BuildingBlocks.Domain.Models;
using BusinessFlow.Domain.BusinessFlowAggregate.Enums;
using BusinessFlow.Domain.BusinessFlowAggregate.Exceptions;
using BusinessFlow.Domain.BusinessFlowAggregate.Models;
using BusinessFlow.Domain.SubmissionExecutionAggregate.Entities;

namespace BusinessFlow.Domain.BusinessFlowAggregate.Entities;

public class BusinessFlowBlock : AggregateRoot<Guid>
{
    public string Name { get; private set; }
    
    public int BusinessFlowVersionId { get; private set; }
    
    public int? FormId { get; private set; }
    
    public BusinessFlowBlockType Type { get; private set; }
    
    public virtual BusinessFlowVersion BusinessFlowVersion { get; private set; } = null!;
    
    public virtual List<BusinessFlowBranch> FromBranches { get; private set; } = new();
    
    public virtual List<BusinessFlowBranch> ToBranches { get; private set; } = new();

    public virtual List<SubmissionExecution> Executions { get; private set; } = new();
    
    public virtual List<BusinessFlowOutCome> OutComes { get; private set; } = new();
    
    public virtual List<BusinessFlowBlockTaskSetting> TaskSettings { get; private set; } = new();
    
    public virtual List<BusinessFlowBlockPersonInChargeSetting> PersonInChargeSettings { get; private set; } = new();
    
    public BusinessFlowBlock(BusinessFlowBlockModel model)
    {
        Id = model.Id;
        Name = model.Name;
        Type = model.Type;
        
        AddOutComes(model.OutComes);
        AddTaskSettings(model.Tasks);
        AddPersonInCharges(model.PersonInChargeIds);
    }
    
    public void AddOutComes(List<BusinessFlowOutComeModel> outComes)
    {
        foreach (var outCome in outComes)
        {
            OutComes.Add(new BusinessFlowOutCome(outCome));
        }
    }
    
    public void AddBranch(BusinessFlowBranch branch)
    {
        if (branch.FromBlockId == Id)
        {
            if (branch.OutComeId.HasValue)
            {
                var outCome = OutComes.FirstOrDefault(o => o.Id == branch.OutComeId);
                if (outCome == null)
                {
                    throw new InvalidBusinessFlowBranchException();
                }
            }
            FromBranches.Add(branch);
        }
        
        if (branch.ToBlockId == Id)
        {
            ToBranches.Add(branch);
        }
    }
    
    public void AddTaskSettings(List<BusinessFlowBlockTaskSettingModel> taskSettings)
    {
        foreach (var taskSetting in taskSettings)
        {
            TaskSettings.Add(new BusinessFlowBlockTaskSetting(taskSetting.Name, taskSetting.Index));
        }
    }
    
    public void AddPersonInCharges(List<string> userIds)
    {
        foreach (var userId in userIds)
        {
            var isExisted = PersonInChargeSettings.Any(p => p.UserId == userId);
            if (isExisted)
            {
                continue;
            }
            
            PersonInChargeSettings.Add(new BusinessFlowBlockPersonInChargeSetting(userId));
        }
    }
    
    public void SetFormId(int formId)
    {
        FormId = formId;
    }

    private BusinessFlowBlock()
    {
        
    }
}