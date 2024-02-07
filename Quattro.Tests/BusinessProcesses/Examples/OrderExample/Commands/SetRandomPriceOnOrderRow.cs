using Quattro.Commands;

namespace Quattro.Tests.BusinessProcesses.Examples.OrderExample.Commands
{
    record SetRandomPriceOnOrderRow(string OrderNumber, int RowPosition)
        : ICommand<SetRandomPriceOnOrderRow.Result>
    {
        public record Result(string OrderNumber, int RowPosition);
    }
}
