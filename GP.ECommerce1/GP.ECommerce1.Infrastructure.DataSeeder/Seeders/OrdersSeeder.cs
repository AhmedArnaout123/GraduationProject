using GP.ECommerce1.Core.Application.Orders.Commands.CreateOrder;
using GP.ECommerce1.Core.Domain;
using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Infrastructure.DataSeeder.Seeders;

public class OrdersSeeder
{
    private readonly IMediator _mediator;

    private static List<Order> Orders { get; set; } = new();

    public OrdersSeeder(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Seed(int count=1000)
    {
        Console.WriteLine("Seeding Orders....");
        var products = await Task.Run(ProductsSeeder.GetAllProducts);
        var customers = await  Task.Run(CustomersSeeder.GetAllCustomers);
        
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

    private static List<OrderItem> GetOrderItems(Guid orderId, List<Product> products)
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

    public static void GenerateAndStoreAsJson(int count = 1000)
    {
        Console.WriteLine("Generating Orders....");
        var products = Task.Run(ProductsSeeder.GetAllProducts).Result;
        var customers = Task.Run(CustomersSeeder.GetAllCustomers).Result;
        List<Order> orders = new();
        for (int i = 0; i < count; i++)
        {
            var id = Guid.NewGuid();
            var items = GetOrderItems(id, products);
            var customer = customers[Randoms.RandomInt(customers.Count)];
            var order = new Order
            {
                Date = Randoms.RandomDate(),
                Id = id,
                CustomerId = customer.Id,
                Status = Enum.GetValues<OrderStatus>()[Randoms.RandomInt(2)],
                Items = items,
                Address = customer.Addresses[Randoms.RandomInt(customer.Addresses.Count)],
                CustomerName = $"{customer.FirstName} {customer.LastName}",
            };
            orders.Add(order);
            Console.WriteLine(i);
        }

        Task.Run(() => FilesHelper.WriteToJsonFile("Orders", orders));
        Orders = orders;
        Console.WriteLine("Generating Orders Finished....");
    }
    
    public static List<Order> GetAllDiscounts()
    {
        if (Orders.Any())
            return Orders;
        var commands = Task.Run(() => FilesHelper.ReadFromJsonFile<List<Order>>("Orders")).Result;
        Orders = commands;
        return commands;
    }
}