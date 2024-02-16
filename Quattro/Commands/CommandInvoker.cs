namespace Quattro.Commands
{
    public class CommandInvoker(IEnumerable<ICommandHandler> handlers)
    {
        private readonly Dictionary<Type, ICommandHandler> _handlers = handlers.ToDictionary(
            h => h.InputType
        );

        public async Task<TOutput> InvokeAsync<TOutput>(ICommand<TOutput> command)
            where TOutput : notnull
        {
            return (TOutput)await InvokeAsync((object)command);
        }

        public async Task<object> InvokeAsync(object command)
        {
            if (!_handlers.TryGetValue(command.GetType(), out var handler))
            {
                throw new InvalidOperationException(
                    $"No handler available for command '{command.GetType().FullName}'"
                );
            }

            return await handler.InvokeAsync(command);
        }
    }
}
