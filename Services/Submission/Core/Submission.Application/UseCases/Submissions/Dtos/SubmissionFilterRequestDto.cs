using BuildingBlocks.Application.Dtos;
using Submission.Application.UseCases.Submissions.Enums;

namespace Submission.Application.UseCases.Submissions.Dtos;

public class SubmissionFilterRequestDto : PagingRequestDto
{
    public string? Search { get; set; }
    
    public int FormVersionId { get; set; }
    
    public List<SubmissionFilterFieldDto>? Filters { get; set; }
}

public class SubmissionFilterFieldDto
{
    public SubmissionFilterFieldType Type { get; set; }
    
    public string Value { get; set; } = null!;
}

public class SubmissionRecordElementFilterFieldDto
{
    public int ElementId { get; set; }
    
    public string Value { get; set; } = null!;
}