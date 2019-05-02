using Microsoft.Extensions.DependencyInjection;
using Reborn.Entities;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;

namespace Reborn.Utility
{
    public static class Loader
    {
        public static readonly IReadOnlyList<Type> ASSEMBLY_CLASSES =
Assembly.GetEntryAssembly().GetTypes().Where(x => x.IsClass && !x.IsNested).ToImmutableArray();

        public static void LoadServices(IServiceCollection services)
        {
            var serviceTypes = ASSEMBLY_CLASSES.Where(x => x.BaseType == typeof(Service));

            foreach (var type in serviceTypes)
                services.AddSingleton(type);
        }

        public static void LoadEvents(IServiceProvider provider)
            => ProviderLoad(provider, typeof(Event));

        public static void ProviderLoad(IServiceProvider provider, Type baseType)
        {
            var serviceTypes = ASSEMBLY_CLASSES.Where(x => x.BaseType == baseType);

            foreach (var service in serviceTypes)
            {
                var ctor = service.GetConstructor(new[] { typeof(IServiceProvider) });

                ctor.Invoke(new[] { provider });
            }
        }
    }
}
