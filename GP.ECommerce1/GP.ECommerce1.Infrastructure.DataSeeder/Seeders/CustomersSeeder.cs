using GP.ECommerce1.Core.Application.Customers.Commands.CreateCustomer;
using GP.ECommerce1.Core.Domain;
using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Infrastructure.DataSeeder.Seeders;

public class CustomersSeeder
{
    private readonly IMediator _mediator;

    private static List<Customer> Customers { get; set; } = new();

    public CustomersSeeder(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Seed(int count = 1000)
    {
        Console.WriteLine("Seeding Customers...");
        for (int i = 1; i <= count; i++)
        {
            var command = new CreateCustomerCommand
            {
                Id = Guid.NewGuid(),
                Email = Randoms.RandomEmail(),
                FirstName = Randoms.RandomString(Randoms.RandomInt(3, 8)),
                LastName = Randoms.RandomString(Randoms.RandomInt(3, 8)),
                Password = Randoms.RandomString(Randoms.RandomInt(20, 25)),
                PhoneNumber = Randoms.RandomPhoneNumber()
            };
            await _mediator.Send(command);
            Console.WriteLine(i);
        }
        Console.WriteLine("Seeding Customers Succeeded...");
        // Console.WriteLine($"Times Consumed: {stopWatch}");
    }

    public static void GenerateAndStoreAsJson(int count)
    {
        Console.WriteLine("Generating Customers");
        List<Customer> customers = new();
        for (int i = 1; i <= count; i++)
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                Email = Randoms.RandomEmail(),
                FirstName = Randoms.RandomString(Randoms.RandomInt(3, 8)),
                LastName = Randoms.RandomString(Randoms.RandomInt(3, 8)),
                PhoneNumber = Randoms.RandomPhoneNumber()
            };
            for (int r = 0; r < Randoms.RandomInt(1,3); r++)
            {
                var address = new Address
                {
                    City = Randoms.RandomString(Randoms.RandomInt(4, 7)),
                    Country = Randoms.RandomString(Randoms.RandomInt(4, 7)),
                    Id = Guid.NewGuid(),
                    State = Randoms.RandomString(Randoms.RandomInt(5, 9)),
                    Street1 = Randoms.RandomSentence(Randoms.RandomInt(5, 8)),
                    Street2 = Randoms.RandomSentence(Randoms.RandomInt(5, 8)),
                    CustomerId = customer.Id
                };
                customer.Addresses.Add(address);
            }
            customers.Add(customer);
            Console.WriteLine(i);
        }

        Task.Run(() => FilesHelper.WriteToJsonFile("Customers", customers)).Wait();
        Customers = customers;
        Console.WriteLine("Generating Customers Succeeded...");
    }

    public static List<Customer> GetAllCustomers()
    {
        if (Customers.Any())
            return Customers;
        var commands = Task.Run(() => FilesHelper.ReadFromJsonFile<List<Customer>>("Customers")).Result;
        Customers = commands;
        return commands;
    }
}