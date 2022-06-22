using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;

namespace MediatR.SimpleInjector.FlowingScope;

public class EmptyRequestPreProcessor<TRequest>
    : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}