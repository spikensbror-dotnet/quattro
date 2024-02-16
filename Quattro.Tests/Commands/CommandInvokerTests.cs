using Quattro.Commands;

namespace Quattro.Tests.Commands
{
    [TestClass]
    public class CommandInvokerTests
    {
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public async Task CommandInvoker_ShouldThrowIfCommandHandlerCannotBeFound()
        {
            var invoker = new CommandInvoker([]);

            await invoker.InvokeAsync(new CommandFixture());
        }

        record CommandFixture() : ICommand<CommandFixture.Result>
        {
            public record Result();
        }
    }
}
