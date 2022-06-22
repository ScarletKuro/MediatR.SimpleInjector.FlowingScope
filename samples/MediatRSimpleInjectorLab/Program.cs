using MediatR;
using MediatR.Pipeline;
using MediatR.SimpleInjector.FlowingScope;
using SimpleInjector;

namespace MediatRSimpleInjectorLab
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start SimpleInjector!");
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            var container = new Container();
            container.Options.DefaultScopedLifestyle = ScopedLifestyle.Flowing;
            container.Register<IMediator, MediatRScoped>(Lifestyle.Scoped);
            container.Register<ISender, MediatRScoped>(Lifestyle.Scoped);
            container.Register<IPublisher, MediatRScoped>(Lifestyle.Scoped);

            container.Register(typeof(IRequestHandler<,>), assemblies);
            
            container.RegisterHandlers(typeof(INotificationHandler<>), assemblies);
            container.RegisterHandlers(typeof(IRequestExceptionAction<,>), assemblies);
            container.RegisterHandlers(typeof(IRequestExceptionHandler<,,>), assemblies);
            container.RegisterHandlers(typeof(IStreamRequestHandler<,>), assemblies);

            //Pipeline
            container.Collection.Register(typeof(IPipelineBehavior<,>), new[]
            {
                typeof(RequestExceptionProcessorBehaviorScoped<,>),
                typeof(RequestExceptionActionProcessorBehaviorScoped<,>),
                typeof(RequestPreProcessorBehavior<,>),
                typeof(RequestPostProcessorBehavior<,>),
            }, Lifestyle.Scoped);
            container.Collection.Register(typeof(IRequestPreProcessor<>), new[] { typeof(EmptyRequestPreProcessor<>) });
            container.Collection.Register(typeof(IRequestPostProcessor<,>), new[] { typeof(EmptyRequestPostProcessor<,>) });

            //container.Register(() => new ServiceFactory(t=> container.GetInstance(t)), Lifestyle.Singleton); // we do not need to register this

            container.Register<Context>(Lifestyle.Scoped);
            container.Register<ControllerTest>(Lifestyle.Transient);

            container.Verify();

            //Simulating multiple requests at same time to verify our scopes don't get messed up
            Parallel.For(1, 11, async number =>
            {
                await using Scope scope = new Scope(container);
                var context = scope.GetInstance<Context>();
                context.SetId(Guid.NewGuid());
                var controller = scope.GetInstance<ControllerTest>();
                await controller.TestExecute();
            });

            Console.WriteLine("End SimpleInjector!");
            Console.WriteLine("-----------------------");
            Console.ReadLine();
        }
    }
}