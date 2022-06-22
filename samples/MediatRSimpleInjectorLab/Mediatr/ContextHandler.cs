using MediatR;

namespace MediatRSimpleInjectorLab.Mediatr;

public class ContextHandler : AsyncRequestHandler<ContextRequest>
{
    public Context Context { get; }

    public ContextHandler(Context context)
    {
        Context = context;
    }
    protected override Task Handle(ContextRequest request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Handler Context Id: {Context.Id}, Thread Id: {Thread.CurrentThread.ManagedThreadId}");
        return Task.CompletedTask;
    }
}