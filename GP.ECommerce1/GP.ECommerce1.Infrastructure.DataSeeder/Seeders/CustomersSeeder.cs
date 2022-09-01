using GP.ECommerce1.Core.Application.Customers.Commands.AddAddress;
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

    public async Task Seed(string fileName)
    {
        Console.WriteLine("Seeding Customers...");
        List<Customer> customers = GetAllCustomers(fileName);
        foreach (var customer in customers)
        {
            var command = new CreateCustomerCommand
            {
                Email = customer.Email,
                Id = customer.Id,
                Password = Randoms.RandomString(8),
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PhoneNumber = customer.PhoneNumber
            };
            await _mediator.Send(command);
            foreach (var address in customer.Addresses)
            {
                var addressCommand = new AddAddressCommand
                {
                    City = address.City,
                    Country = address.Country,
                    Id = address.Id,
                    State = address.State,
                    Street1 = address.Street1,
                    Street2 = address.Street2,
                    CustomerId = address.CustomerId
                };
                await _mediator.Send(addressCommand);
            }
        }
        Console.WriteLine("Seeding Customers Finished...");
        // Console.WriteLine($"Times Consumed: {stopWatch}");
    }

    public static void GenerateAndStoreAsJson(string fileName, int count)
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

        Task.Run(() => FilesHelper.WriteToJsonFile(fileName, customers)).Wait();
        Customers = customers;
        Console.WriteLine("Generating Customers Succeeded...");
    }

    public static List<Customer> GetAllCustomers(string fileName)
    {
        if (Customers.Any())
            return Customers;
        var commands = Task.Run(() => FilesHelper.ReadFromJsonFile<List<Customer>>(fileName)).Result;
        Customers = commands;
        return commands;
    }
}