using Quattro.Commands;
using Quattro.Tests.BusinessProcesses.Examples.OrderExample.Persistence;

namespace Quattro.Tests.BusinessProcesses.Examples.OrderExample.Commands
{
    class CreateOrderHandler(InMemoryOrderRepository orderRepository)
        : CommandHandler<TestContext, CreateOrder, CreateOrder.Result>
    {
        public override async Task<CreateOrder.Result> InvokeAsync(
            TestContext context,
            CreateOrder input
        )
        {
            var orderNumber = Guid.NewGuid().ToString()[..8].ToUpperInvariant();

            var order = new Order(orderNumber, input.CustomerNumber, input.Currency, []);
            await orderRepository.SaveAsync(order);

            return new(orderNumber);
        }
    }
}
