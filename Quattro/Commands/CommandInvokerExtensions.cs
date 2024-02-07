namespace Quattro.Commands
{
    public static class CommandInvokerExtensions
    {
        public static async Task<IEnumerable<TOutput>> InvokeInOrderAsync<TContext, TOutput>(
            this CommandInvoker invoker,
            TContext context,
            IEnumerable<ICommand<TOutput>> commands
        )
            where TContext : notnull
            where TOutput : notnull
        {
            var results = Array.Empty<TOutput>();
            foreach (var command in commands)
            {
                results = [.. results, await invoker.InvokeAsync(context, command)];
            }

            return results;
        }

        public static async Task<IEnumerable<TOutput>> InvokeInParallelAsync<TContext, TOutput>(
            this CommandInvoker invoker,
            TContext context,
            IEnumerable<ICommand<TOutput>> commands
        )
            where TContext : notnull
            where TOutput : notnull
        {
            var tasks = Array.Empty<Task<TOutput>>();
            foreach (var command in commands)
            {
                tasks = [.. tasks, invoker.InvokeAsync(context, command)];
            }

            return await Task.WhenAll(tasks);
        }
    }
}
