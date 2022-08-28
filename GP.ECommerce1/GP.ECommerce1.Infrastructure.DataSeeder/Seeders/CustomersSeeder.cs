using GP.ECommerce1.Core.Application.Customers.Commands.CreateCustomer;
using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Infrastructure.DataSeeder.Seeders;

public class CustomersSeeder
{
    private readonly IMediator _mediator;

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
}