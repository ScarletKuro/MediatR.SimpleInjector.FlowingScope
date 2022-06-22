using System;
using System.Reflection;
using SimpleInjector;

namespace MediatR.SimpleInjector.FlowingScope
{
    public static class ContainerExtensions
    {
        public static void RegisterHandlers(this Container container, Type collectionType, Assembly[] assemblies)
        {
            // we have to do this because by default, generic type definitions (such as the Constrained Notification Handler) won't be registered
            var handlerTypes = container.GetTypesToRegister(collectionType, assemblies, new TypesToRegisterOptions
            {
                IncludeGenericTypeDefinitions = true,
                IncludeComposites = false,
            });

            container.Collection.Register(collectionType, handlerTypes);
        }
    }
}
