using GP.ECommerce1.Core.Application.Customers.Commands.AddAddress;

using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Infrastructure.DataSeeder.Seeders;

public class AddressesSeeder
{
    private readonly IMediator _mediator;
    private readonly DataSeedingHelper _dataSeedingHelper;

    public AddressesSeeder(IMediator mediator, DataSeedingHelper dataSeedingHelper)
    {
        _mediator = mediator;
        _dataSeedingHelper = dataSeedingHelper;
    }

    public async Task Seed()
    {
        Console.WriteLine("Seeding Addresses....");
        var customers = await _dataSeedingHelper.GetAllCustomers();
        foreach (var customer in customers)
        {
            for (int i = 0; i < Randoms.RandomInt(1,5); i++)
            {
                var command = new AddAddressCommand
                {
                    City = Randoms.RandomString(Randoms.RandomInt(4, 7)),
                    Country = Randoms.RandomString(Randoms.RandomInt(4, 7)),
                    Id = Guid.NewGuid(),
                    State = Randoms.RandomString(Randoms.RandomInt(5, 9)),
                    Street1 = Randoms.RandomSentence(Randoms.RandomInt(5, 8)),
                    Street2 = Randoms.RandomSentence(Randoms.RandomInt(5, 8)),
                    CustomerId = customer.Id
                };
                await _mediator.Send(command);
            }
        }
        Console.WriteLine("Seeding Addresses Finished....");
    }
}