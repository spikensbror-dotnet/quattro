namespace Quattro.Commands
{
    public interface ICommandHandler
    {
        /// <summary>
        /// The context type defines which type the command handler expects
        /// to receive as its context arguments.
        ///
        /// An example use case for the context is access to a request-scoped
        /// transaction to be used for any database CUD operations.
        /// </summary>
        internal Type ContextType { get; }

        /// <summary>
        /// The input type expected by the command handler.
        /// </summary>
        internal Type InputType { get; }

        /// <summary>
        /// Invokes the command handler using the provided context and command.
        /// </summary>
        /// <param name="context">The context of the command invokation.</param>
        /// <param name="command">The command to invoke.</param>
        /// <returns>The result of the command invokation.</returns>
        internal Task<object> InvokeAsync(object context, object command);
    }
}
