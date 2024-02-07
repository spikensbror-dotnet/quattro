using System.Reflection;
using Quattro.Commands;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static void AddQuattro(
            this IServiceCollection services,
            params Assembly[] assemblies
        )
        {
            services.AddSingleton(typeof(CommandInvoker));

            services.AddQuattroCommandHandlers(assemblies);
            services.AddQuattroBusinessProcesses(assemblies);
        }

        private static void AddQuattroCommandHandlers(
            this IServiceCollection services,
            Assembly[] assemblies
        )
        {
            var commandHandlerTypes = assemblies
                .SelectMany(a => a.DefinedTypes)
                .Where(t => t.IsAssignableTo(typeof(ICommandHandler)) && t.IsClass && !t.IsAbstract)
                .Select(t => t.AsType())
                .ToArray();

            foreach (var type in commandHandlerTypes)
            {
                services.AddSingleton(typeof(ICommandHandler), type);
            }
        }

        private static void AddQuattroBusinessProcesses(
            this IServiceCollection services,
            Assembly[] assemblies
        )
        {
            var businessProcessTypes = assemblies
                .SelectMany(a => a.DefinedTypes)
                .Where(
                    t => t.IsClass && !t.IsAbstract && t.GetInterface("IBusinessProcess`3") != null
                )
                .Select(t => t.AsType())
                .ToArray();

            foreach (var type in businessProcessTypes)
            {
                services.AddSingleton(type);
                var interfaceType = type.GetInterface("IBusinessProcess`3")!;
                services.AddSingleton(interfaceType, sp => sp.GetService(type)!);
            }
        }
    }
}
