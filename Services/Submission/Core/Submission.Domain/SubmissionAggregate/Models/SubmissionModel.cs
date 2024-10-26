﻿namespace Submission.Domain.SubmissionAggregate.Models;

public class SubmissionModel
{
    public string Name { get; set; } = null!;
    
    public int FormVersionId { get; set; }
    
    public string? TrackingToken { get; set; }
    
    public string? TrackingEmail { get; set; }
    
    public List<SubmissionFieldModel> Fields { get; set; } = null!;
}