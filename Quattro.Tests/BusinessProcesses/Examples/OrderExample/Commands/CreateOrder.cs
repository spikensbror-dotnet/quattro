using Quattro.Commands;

namespace Quattro.Tests.BusinessProcesses.Examples.OrderExample.Commands
{
    record CreateOrder(string CustomerNumber, string Currency) : ICommand<CreateOrder.Result>
    {
        public record Result(string OrderNumber);
    }
}
