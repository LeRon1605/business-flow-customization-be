using System.Collections.Immutable;
using Microsoft.Extensions.DependencyInjection;
using Submission.Domain.FormAggregate.Enums;
using Submission.Domain.SubmissionAggregate.DomainServices.Abstracts;

namespace Submission.Domain.SubmissionAggregate.DomainServices;

public class SubmissionFieldCreatorFactory : ISubmissionFieldCreatorFactory
{
    private ImmutableDictionary<FormElementType, ISubmissionFieldCreator> Creators { get; }

    public SubmissionFieldCreatorFactory(IServiceProvider serviceProvider)
    {
        Creators = typeof(ISubmissionFieldCreator)
            .Assembly
            .ExportedTypes
            .Where(x =>
                typeof(ISubmissionFieldCreator).IsAssignableFrom(x)
                && x is { IsAbstract: false, IsInterface: false }
            )
            .Select(x => ActivatorUtilities.CreateInstance(serviceProvider, x))
            .Cast<ISubmissionFieldCreator>()
            .ToImmutableDictionary(x => x.Type, x => x);
    }
    
    public ISubmissionFieldCreator Get(FormElementType type)
    {
        return Creators[type];
    }
}