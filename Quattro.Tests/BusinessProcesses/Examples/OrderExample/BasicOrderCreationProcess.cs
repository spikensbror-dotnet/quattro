using Quattro.BusinessProcesses;
using Quattro.Commands;
using Quattro.Tests.BusinessProcesses.Examples.OrderExample.Commands;

namespace Quattro.Tests.BusinessProcesses.Examples.OrderExample
{
    class BasicOrderCreationProcess(CommandInvoker commands)
        : IBusinessProcess<
            TestContext,
            BasicOrderCreationProcess.Input,
            BasicOrderCreationProcess.Output
        >
    {
        public async Task<Output> InvokeAsync(TestContext context, Input input)
        {
            var order = await commands.InvokeAsync(context, input.Order);
            var rows = await commands.InvokeInOrderAsync(
                context,
                from row in input.Rows
                select row with
                {
                    OrderNumber = order.OrderNumber
                }
            );

            await commands.InvokeInParallelAsync(
                context,
                from row in rows
                select new SetRandomPriceOnOrderRow(row.OrderNumber, row.RowPosition)
            );

            return new(order, rows.ToArray());
        }

        public record Input(CreateOrder Order, CreateOrderRow[] Rows);

        public record Output(CreateOrder.Result Order, CreateOrderRow.Result[] Rows);
    }
}
