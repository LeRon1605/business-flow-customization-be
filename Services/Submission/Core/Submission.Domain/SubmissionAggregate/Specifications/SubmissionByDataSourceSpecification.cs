using System.Linq.Expressions;
using BuildingBlocks.Domain.Specifications;
using Domain.Identities;
using LinqKit;
using Submission.Domain.SubmissionAggregate.Entities;
using Submission.Domain.SubmissionAggregate.Enums;

namespace Submission.Domain.SubmissionAggregate.Specifications;

public class SubmissionByDataSourceSpecification : Specification<FormSubmission>
{
    private readonly List<SubmissionDataSource> _dataSources;
    
    public SubmissionByDataSourceSpecification(List<SubmissionDataSource> dataSources)
    {
        _dataSources = dataSources;
    }
    
    public override Expression<Func<FormSubmission, bool>> ToExpression()
    {
        var predicate = PredicateBuilder.New<FormSubmission>(x => false);
        foreach (var dataSource in _dataSources)
        {
            switch (dataSource)
            {
                case SubmissionDataSource.Internal:
                    predicate = predicate.Or(x => x.CreatedBy != AppUser.SystemUser);
                    break;
                
                case SubmissionDataSource.External:
                    predicate = predicate.Or(x => x.CreatedBy == AppUser.SystemUser);
                    break;
            }
        }

        return predicate;
    }
}