using Quattro.BusinessProcesses;
using Quattro.Commands;
using Quattro.Tests.BusinessProcesses.Examples.OrderExample.Commands;

namespace Quattro.Tests.BusinessProcesses.Examples.OrderExample
{
    class BasicOrderCreationProcess(CommandInvoker commands)
        : IBusinessProcess<BasicOrderCreationProcess.Input, BasicOrderCreationProcess.Output>
    {
        public async Task<Output> InvokeAsync(Input input)
        {
            var order = await commands.InvokeAsync(input.Order);
            var rows = await commands.InvokeInOrderAsync(
                from row in input.Rows
                select row with
                {
                    OrderNumber = order.OrderNumber
                }
            );

            await commands.InvokeInParallelAsync(
                from row in rows
                select new SetRandomPriceOnOrderRow(row.OrderNumber, row.RowPosition)
            );

            return new(order, rows.ToArray());
        }

        public record Input(CreateOrder Order, CreateOrderRow[] Rows);

        public record Output(CreateOrder.Result Order, CreateOrderRow.Result[] Rows);
    }
}
