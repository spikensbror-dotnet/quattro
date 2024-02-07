using Quattro.Commands;
using Quattro.Tests.BusinessProcesses.Examples.OrderExample.Persistence;

namespace Quattro.Tests.BusinessProcesses.Examples.OrderExample.Commands
{
    class CreateOrderRowHandler(InMemoryOrderRepository orderRepository)
        : CommandHandler<TestContext, CreateOrderRow, CreateOrderRow.Result>
    {
        public override async Task<CreateOrderRow.Result> InvokeAsync(
            TestContext context,
            CreateOrderRow input
        )
        {
            if (input.OrderNumber == null)
            {
                throw new InvalidOperationException();
            }

            var order = await orderRepository.GetAsync(input.OrderNumber);
            var row = new Order.Row(
                input.ArticleNumber,
                input.Description,
                input.Quantity,
                input.Price,
                input.Discount
            );

            await orderRepository.SaveAsync(order with { Rows = [.. order.Rows, row] });

            return new(order.OrderNumber, order.Rows.Length);
        }
    }
}
