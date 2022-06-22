# MediatR.SimpleInjector.FlowingScope
[![Nuget](https://img.shields.io/nuget/v/MediatR.SimpleInjector.FlowingScope?color=ff4081&logo=nuget)](https://www.nuget.org/packages/MediatR.SimpleInjector.FlowingScope/)
[![Nuget](https://img.shields.io/nuget/dt/MediatR.SimpleInjector.FlowingScope?color=ff4081&label=nuget%20downloads&logo=nuget)](https://www.nuget.org/packages/MediatR.SimpleInjector.FlowingScope/)
[![GitHub](https://img.shields.io/github/license/ScarletKuro/MediatR.SimpleInjector.FlowingScope?color=594ae2&logo=github)](https://github.com/ScarletKuro/MediatR.SimpleInjector.FlowingScope/blob/master/LICENSE)

Adds support to use MediatR with SimpleInjector's ScopedLifestyle.Flowing.

## 📜Samples
1. [MediatRSimpleInjectorLab](https://github.com/ScarletKuro/MediatR.SimpleInjector.FlowingScope/tree/master/samples/MediatRSimpleInjectorLab) - Minimal getting started project.

## 🚀Getting Started
### Register MediatR
You need register everything by yourself. There is no helper class for that, because some configuration is up to the user.
Example:
```CSharp
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
```

### Resolving Scope
```CSharp
using Scope scope = new Scope(container);
// You will have to resolve directly from the scope.
var instance = scope.GetInstance<ScopedInstance>();
```
For more details refer to SimpleInjector [documentation](https://docs.simpleinjector.org/en/latest/lifetimes.html#flowing-scopes)
