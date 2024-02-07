namespace Quattro.Commands
{
    public abstract class CommandHandler<TContext, TInput, TOutput> : ICommandHandler
        where TInput : ICommand<TOutput>
        where TOutput : notnull
    {
        public Type ContextType => typeof(TContext);
        public Type InputType => typeof(TInput);

        public abstract Task<TOutput> InvokeAsync(TContext context, TInput input);

        public async Task<object> InvokeAsync(object context, object command)
        {
            return await InvokeAsync((TContext)context, (TInput)command);
        }
    }
}
