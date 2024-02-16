namespace Quattro.Commands
{
    public abstract class CommandHandler<TInput, TOutput> : ICommandHandler
        where TInput : ICommand<TOutput>
        where TOutput : notnull
    {
        public Type InputType => typeof(TInput);

        public abstract Task<TOutput> InvokeAsync(TInput input);

        public async Task<object> InvokeAsync(object command)
        {
            return await InvokeAsync((TInput)command);
        }
    }
}
