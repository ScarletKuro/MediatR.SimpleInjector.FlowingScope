using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;

namespace MediatR.SimpleInjector.FlowingScope;

public class EmptyRequestPostProcessor<TRequest, TResponse>
    : IRequestPostProcessor<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}