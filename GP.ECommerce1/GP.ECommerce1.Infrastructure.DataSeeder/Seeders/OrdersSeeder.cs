using GP.ECommerce1.Core.Application.Orders.Commands.CreateOrder;
using GP.ECommerce1.Core.Domain;
using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Infrastructure.DataSeeder.Seeders;

public class OrdersSeeder
{
    private readonly IMediator _mediator;
    private readonly DataSeedingHelper _dataSeedingHelper;

    public OrdersSeeder(IMediator mediator, DataSeedingHelper dataSeedingHelper)
    {
        _mediator = mediator;
        _dataSeedingHelper = dataSeedingHelper;
    }

    public async Task Seed(int count=1000)
    {
        Console.WriteLine("Seeding Orders....");
        var customers = await _dataSeedingHelper.GetAllCustomers();
        var products = await _dataSeedingHelper.GetAllProducts();
        var statues = await _dataSeedingHelper.GetAllOrderStatuses();
        
        for (int i = 0; i < count; i++)
        {
            var id = Guid.NewGuid();
            var items = GetOrderItems(id, products);
            var command = new CreateOrderCommand
            {
                Date = Randoms.RandomDate(),
                Id = id,
                CustomerId = customers[Randoms.RandomInt(customers.Count)].Id,
                StatusId = statues[Randoms.RandomInt(statues.Count)].Id,
                Items = items,
                Subtotal = items.Sum(o => o.ProductSubtotal)
            };
            await _mediator.Send(command);
            Console.WriteLine(i);
        }
        Console.WriteLine("Seeding Orders Finished....");
    }

    private List<OrderItem> GetOrderItems(Guid orderId, List<Product> products)
    {
        List<OrderItem> items = new();
        for (int i = 0; i < Randoms.RandomInt(1, 3); i++)
        {
            var product = products[Randoms.RandomInt(products.Count)];
            var item = new OrderItem()
            {
                Quantity = Randoms.RandomQuantity(),
                OrderId = orderId,
                ProductName = product.Name,
                DiscountId = product.DiscountId,
                ProductId = product.Id,
                ProductPrice = product.Price,
                ProductSubtotal = product.DiscountId == null
                    ? product.Price
                    : product.Price * product.DiscountPercentage!.Value / 100
            };
            items.Add(item);
        }

        return items;
    }
}