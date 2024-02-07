namespace Quattro.Tests.BusinessProcesses.Examples.OrderExample.Persistence
{
    class InMemoryOrderRepository
    {
        private readonly Dictionary<string, Order> orders = [];

        public Task<Order[]> GetAsync()
        {
            return Task.FromResult(orders.Values.ToArray());
        }

        public Task<Order> GetAsync(string orderNumber)
        {
            return Task.FromResult(
                orders.TryGetValue(orderNumber, out var order)
                    ? order
                    : throw new InvalidOperationException(
                        $"Order number '{orderNumber}' does not exist"
                    )
            );
        }

        public Task SaveAsync(Order order)
        {
            orders[order.OrderNumber] = order;

            return Task.CompletedTask;
        }

        public Task<bool> DeleteAsync(string orderNumber)
        {
            return Task.FromResult(orders.Remove(orderNumber));
        }
    }
}
