namespace Quattro.BusinessProcesses
{
    public interface IBusinessProcess<TContext, TInput, TOutput>
    {
        Task<TOutput> InvokeAsync(TContext context, TInput input);
    }
}
