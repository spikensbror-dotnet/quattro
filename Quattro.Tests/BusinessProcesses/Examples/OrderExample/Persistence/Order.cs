namespace Quattro.Tests.BusinessProcesses.Examples.OrderExample.Persistence
{
    record Order(string OrderNumber, string CustomerNumber, string Currency, Order.Row[] Rows)
    {
        public record Row(
            string ArticleNumber,
            string Description,
            decimal Quantity,
            decimal Price,
            decimal Discount = 0M
        );
    }
}
