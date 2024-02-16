namespace Quattro.Commands
{
    public static class CommandInvokerExtensions
    {
        public static async Task<IEnumerable<TOutput>> InvokeInOrderAsync<TOutput>(
            this CommandInvoker invoker,
            IEnumerable<ICommand<TOutput>> commands
        )
            where TOutput : notnull
        {
            var results = Array.Empty<TOutput>();
            foreach (var command in commands)
            {
                results = [.. results, await invoker.InvokeAsync(command)];
            }

            return results;
        }

        public static async Task<IEnumerable<TOutput>> InvokeInParallelAsync<TOutput>(
            this CommandInvoker invoker,
            IEnumerable<ICommand<TOutput>> commands
        )
            where TOutput : notnull
        {
            var tasks = Array.Empty<Task<TOutput>>();
            foreach (var command in commands)
            {
                tasks = [.. tasks, invoker.InvokeAsync(command)];
            }

            return await Task.WhenAll(tasks);
        }
    }
}
