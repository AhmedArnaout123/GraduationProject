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

    public async Task Seed(string fileName)
    {
        Console.WriteLine("Seeding Orders....");
        var orders = GetAllOrders(fileName);
        int counter = 0;
        foreach(var order in orders)
        {
            var command = new CreateOrderCommand
            {
                Date = order.Date,
                Id = order.Id,
                CustomerId = order.CustomerId,
                Status = order.Status,
                Items = order.Items,
                Address = order.Address,
                CustomerName = order.CustomerName
            };
            await _mediator.Send(command);
            Console.WriteLine(++counter);
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

    public static void GenerateAndStoreAsJson(string fileName, string customersFileName, string productsFileName, int count)
    {
        Console.WriteLine("Generating Orders....");
        var products = Task.Run(() => ProductsSeeder.GetAllProducts(productsFileName)).Result;
        var customers = Task.Run(() => CustomersSeeder.GetAllCustomers(customersFileName)).Result;
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

        Task.Run(() => FilesHelper.WriteToJsonFile(fileName, orders));
        Orders = orders;
        Console.WriteLine("Generating Orders Finished....");
    }
    
    public static List<Order> GetAllOrders(string fileName)
    {
        if (Orders.Any())
            return Orders;
        var commands = Task.Run(() => FilesHelper.ReadFromJsonFile<List<Order>>(fileName)).Result;
        Orders = commands;
        return commands;
    }
}