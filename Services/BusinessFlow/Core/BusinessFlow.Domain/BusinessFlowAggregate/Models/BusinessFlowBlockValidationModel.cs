namespace BusinessFlow.Domain.BusinessFlowAggregate.Models;

public class BusinessFlowBlockValidationModel<TKey> where TKey : IEquatable<TKey>
{
    public TKey? Id { get; set; }

    public List<string> ErrorMessages { get; set; } = new();
}