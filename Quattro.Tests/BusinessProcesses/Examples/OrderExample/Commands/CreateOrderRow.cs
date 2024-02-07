using Quattro.Commands;

namespace Quattro.Tests.BusinessProcesses.Examples.OrderExample.Commands
{
    record CreateOrderRow(
        string ArticleNumber,
        string Description,
        decimal Quantity,
        decimal Price,
        decimal Discount = 0,
        string? OrderNumber = null
    ) : ICommand<CreateOrderRow.Result>
    {
        public record Result(string OrderNumber, int RowPosition);
    }
}
