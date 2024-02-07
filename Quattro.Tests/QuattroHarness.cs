using Microsoft.Extensions.DependencyInjection;
using Quattro.Tests.BusinessProcesses.Examples.OrderExample.Persistence;

namespace Quattro.Tests
{
    static class QuattroHarness
    {
        private static ServiceProvider? serviceProvider;

        public static T Resolve<T>()
        {
            return (serviceProvider ??= Bootstrap()).GetService<T>()
                ?? throw new InvalidOperationException($"{typeof(T).Name} is not registered");
        }

        private static ServiceProvider Bootstrap()
        {
            var services = new ServiceCollection();
            services.AddSingleton<InMemoryOrderRepository>();
            services.AddQuattro(typeof(QuattroHarness).Assembly);

            return services.BuildServiceProvider();
        }
    }
}
