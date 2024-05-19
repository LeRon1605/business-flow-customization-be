namespace Submission.Domain.SubmissionAggregate.Abstracts;

public interface ISubmissionField
{
    public int SubmissionId { get; set; }
    
    public int ElementId { get; set; }
}

public interface ISubmissionField<in T> : ISubmissionField
{
    public void UpdateValue(T field);
}