using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SimpleInjector;

namespace MediatR.SimpleInjector.FlowingScope
{
    public class MediatRScoped : IMediator
    {
        private readonly IMediator _mediator;

        public MediatRScoped(Scope scope)
        {
            _mediator = new Mediator(scope.GetInstance);
        }

        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            return _mediator.Send(request, cancellationToken);
        }

        public Task<object?> Send(object request, CancellationToken cancellationToken = default)
        {
            return _mediator.Send(request, cancellationToken);
        }

        public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            return _mediator.CreateStream(request, cancellationToken);
        }

        public IAsyncEnumerable<object?> CreateStream(object request, CancellationToken cancellationToken = default)
        {
            return _mediator.CreateStream(request, cancellationToken);
        }

        public Task Publish(object notification, CancellationToken cancellationToken = default)
        {
            return _mediator.Publish(notification, cancellationToken);
        }

        public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
        {
            return _mediator.Publish(notification, cancellationToken);
        }
    }
}
