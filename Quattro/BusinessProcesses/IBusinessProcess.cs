namespace Quattro.BusinessProcesses
{
    public interface IBusinessProcess<TInput, TOutput>
    {
        Task<TOutput> InvokeAsync(TInput input);
    }
}
