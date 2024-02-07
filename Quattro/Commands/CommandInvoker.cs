namespace Quattro.Commands
{
    public class CommandInvoker(IEnumerable<ICommandHandler> handlers)
    {
        private readonly Dictionary<(Type, Type), ICommandHandler> _handlers =
            handlers.ToDictionary(h => (h.ContextType, h.InputType));

        public async Task<TOutput> InvokeAsync<TContext, TOutput>(
            TContext context,
            ICommand<TOutput> command
        )
            where TContext : notnull
            where TOutput : notnull
        {
            return (TOutput)await InvokeAsync(context, (object)command);
        }

        public async Task<object> InvokeAsync(object context, object command)
        {
            if (!_handlers.TryGetValue((context.GetType(), command.GetType()), out var handler))
            {
                throw new InvalidOperationException(
                    $"No handler available for command '{command.GetType().FullName}' with context type '{context.GetType().FullName}'"
                );
            }

            return await handler.InvokeAsync(context, command);
        }
    }
}
