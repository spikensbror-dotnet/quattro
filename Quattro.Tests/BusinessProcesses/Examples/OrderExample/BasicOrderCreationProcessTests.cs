using Quattro.Tests.BusinessProcesses.Examples.OrderExample.Commands;
using Quattro.Tests.BusinessProcesses.Examples.OrderExample.Persistence;

namespace Quattro.Tests.BusinessProcesses.Examples.OrderExample
{
    [TestClass]
    public class BasicOrderCreationProcessTests
    {
        [TestMethod]
        public async Task ShouldCreateOrderWithSpecifiedRows()
        {
            var output = await QuattroHarness
                .Resolve<BasicOrderCreationProcess>()
                .InvokeAsync(
                    new BasicOrderCreationProcess.Input(
                        new CreateOrder(CustomerNumber: "202", Currency: "SEK"),
                        [
                            new CreateOrderRow(
                                ArticleNumber: "01",
                                Description: "Test article 1",
                                Quantity: 42M,
                                Price: 0M,
                                Discount: 10M
                            ),
                            new CreateOrderRow(
                                ArticleNumber: "02",
                                Description: "Test article 2",
                                Quantity: 10M,
                                Price: 0M,
                                Discount: 5M
                            ),
                        ]
                    )
                );

            var order = await QuattroHarness
                .Resolve<InMemoryOrderRepository>()
                .GetAsync(output.Order.OrderNumber);

            Assert.AreEqual(output.Order.OrderNumber, order.OrderNumber);
            Assert.AreEqual("202", order.CustomerNumber);

            // Price should be randomly generated as part of the business process, hence
            // there should be at least two rows with different prices.
            var priceSet = order.Rows.Select(r => r.Price).ToHashSet();
            Assert.IsTrue(priceSet.Count > 1, "Prices do not appear to be randomly generated.");
        }
    }
}
