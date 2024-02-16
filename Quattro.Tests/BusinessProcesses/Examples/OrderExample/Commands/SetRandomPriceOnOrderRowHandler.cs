using Quattro.Commands;
using Quattro.Tests.BusinessProcesses.Examples.OrderExample.Persistence;

namespace Quattro.Tests.BusinessProcesses.Examples.OrderExample.Commands
{
    class SetRandomPriceOnOrderRowHandler(InMemoryOrderRepository orderRepository)
        : CommandHandler<SetRandomPriceOnOrderRow, SetRandomPriceOnOrderRow.Result>
    {
        public override async Task<SetRandomPriceOnOrderRow.Result> InvokeAsync(
            SetRandomPriceOnOrderRow input
        )
        {
            var order = await orderRepository.GetAsync(input.OrderNumber);

            order = order with
            {
                Rows =
                [
                    .. order.Rows,
                    order.Rows[input.RowPosition] with
                    {
                        Price = Math.Round(10M + ((decimal)new Random().NextDouble() * 990M), 2),
                    }
                ]
            };

            await orderRepository.SaveAsync(order);

            return new(input.OrderNumber, input.RowPosition);
        }
    }
}
