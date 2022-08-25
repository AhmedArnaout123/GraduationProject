using GP.ECommerce1.Core.Application.Reviews.Commands.CreateReview;
using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Infrastructure.DataSeeder.Seeders;

public class ReviewsSeeder
{
    private readonly IMediator _mediator;
    private readonly DataSeedingHelper _seedingHelper;

    public ReviewsSeeder(IMediator mediator, DataSeedingHelper seedingHelper)
    {
        _mediator = mediator;
        _seedingHelper = seedingHelper;
    }

    public async Task Seed(int count=1000)
    {
        Console.WriteLine("Seeding Reviews....");
        var products = await _seedingHelper.GetAllProducts();
        var customers = await _seedingHelper.GetAllCustomers();
        for (int i = 0; i < count; i++)
        {
            var command = new CreateReviewCommand
            {
                Comment = Randoms.RandomSentence(30),
                Id = Guid.NewGuid(),
                Rate = Randoms.RandomRate(),
                CustomerId = customers[Randoms.RandomInt(customers.Count)].Id,
                ProductId = products[Randoms.RandomInt(products.Count)].Id
            };
            await _mediator.Send(command);
        }
        Console.WriteLine("Seeding Reviews Succeeded....");
    }
}