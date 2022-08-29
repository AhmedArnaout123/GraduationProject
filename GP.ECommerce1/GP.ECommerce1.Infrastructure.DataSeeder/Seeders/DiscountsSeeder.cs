using GP.ECommerce1.Core.Application.Discounts.Commands.CreateDiscount;
using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Infrastructure.DataSeeder.Seeders;

public class DiscountsSeeder
{
    private readonly IMediator _mediator;

    public DiscountsSeeder(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Seed()
    {
        Console.WriteLine("Seeding Discounts....");
        var percentages = new [] {10, 15, 20, 25, 30, 35, 40, 50, 60, 70};
        foreach (var percentage in percentages)
        {
            var command = new CreateDiscountCommand
            {
                Id = Guid.NewGuid(),
                Percentage = percentage,
                Description = Randoms.RandomSentence(6)
            };
            await _mediator.Send(command);
        }
        Console.WriteLine("Seeding Discounts Succeeded....");
    }
}