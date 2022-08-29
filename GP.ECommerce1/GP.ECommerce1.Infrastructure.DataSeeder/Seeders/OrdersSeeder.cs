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
        
        for (int i = 0; i < count; i++)
        {
            var id = Guid.NewGuid();
            var items = GetOrderItems(id, products);
            var customer = customers[Randoms.RandomInt(customers.Count)];
            var command = new CreateOrderCommand
            {
                Date = Randoms.RandomDate(),
                Id = id,
                CustomerId = customer.Id,
                Status = Enum.GetValues<OrderStatus>()[Randoms.RandomInt(2)],
                Items = items,
                Subtotal = items.Sum(o => o.ProductSubtotal),
                Address = customer.Addresses[Randoms.RandomInt(customer.Addresses.Count)],
                CustomerName = $"{customer.FirstName} {customer.LastName}"
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
            var item = new OrderItem
            {
                Quantity = Randoms.RandomQuantity(),
                OrderId = orderId,
                ProductName = product.Name,
                Discount = product.Discount,
                ProductId = product.Id,
                ProductPrice = product.Price,
            };
            items.Add(item);
        }

        return items;
    }
}