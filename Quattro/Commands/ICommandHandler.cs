namespace Quattro.Commands
{
    public interface ICommandHandler
    {
        /// <summary>
        /// The input type expected by the command handler.
        /// </summary>
        Type InputType { get; }

        /// <summary>
        /// Invokes the command handler using the provided context and command.
        /// </summary>
        /// <param name="command">The command to invoke.</param>
        /// <returns>The result of the command invokation.</returns>
        Task<object> InvokeAsync(object command);
    }
}
