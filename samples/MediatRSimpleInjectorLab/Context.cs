namespace MediatRSimpleInjectorLab;

public class Context
{
    public Guid Id { get; private set; }

    public void SetId(Guid id)
    {
        Id = id;
    }
}