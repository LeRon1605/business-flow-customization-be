using BuildingBlocks.Domain.Models;
using Domain.Enums;
using Submission.Domain.FormAggregate.Exceptions;
using Submission.Domain.FormAggregate.Models;

namespace Submission.Domain.FormAggregate.Entities;

public class FormElement : TenantEntity
{
    public string Name { get; private set; }
    
    public string Description { get; private set; }
    
    public FormElementType Type { get; private set; }
    
    public double Index { get; private set; }
    
    public int FormVersionId { get; private set; }

    public virtual FormVersion FormVersion { get; private set; } = null!;
    
    public virtual List<FormElementSetting> Settings { get; private set; } = new();
    
    public virtual List<OptionFormElementSetting> Options { get; private set; } = new();
    
    public FormElement(FormElementModel elementModel)
    {
        Name = elementModel.Name;
        Description = elementModel.Description;
        Type = elementModel.Type;
        Index = elementModel.Index;
        SetElementSetting(elementModel);
    }

    private void SetElementSetting(FormElementModel elementModel)
    {
        ApplyCommonSetting(elementModel.Settings);

        switch (elementModel.Type)
        {
            case FormElementType.SingleOption:
            case FormElementType.MultiOption:
                ApplyOptionSetting(elementModel.Options);
                break;
        }
    }
    
    private void ApplyCommonSetting(List<FormElementSettingModel> settings)
    {
        foreach (var setting in settings)
        {
            var isExisted = Settings.Any(x => x.Type == setting.Type);
            if (isExisted)
            {
                throw new InvalidFormElementSettingException();
            }
            
            Settings.Add(new FormElementSetting(setting));
        }
    }

    private void ApplyOptionSetting(List<OptionFormElementSettingModel> options)
    {
        foreach (var option in options)
        {
            var isExisted = Options.Any(x => x.Name == option.Name);
            if (isExisted)
            {
                throw new InvalidFormElementSettingException();
            }
            
            Options.Add(new OptionFormElementSetting(option.Name));
        }
    }
    
    private FormElement()
    {
    }
}