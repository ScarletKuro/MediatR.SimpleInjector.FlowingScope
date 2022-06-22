using MediatR;
using MediatRSimpleInjectorLab.Mediatr;

namespace MediatRSimpleInjectorLab;

public class ControllerTest
{
    private readonly Context _context;
    private readonly ISender _sender;

    public ControllerTest(Context context, ISender sender)
    {
        _context = context;
        _sender = sender;
    }

    public Task TestExecute()
    {
        Console.WriteLine($"Controller Context Id: {_context.Id}, Thread Id: {Thread.CurrentThread.ManagedThreadId}");
        return _sender.Send(new ContextRequest());
    }
}