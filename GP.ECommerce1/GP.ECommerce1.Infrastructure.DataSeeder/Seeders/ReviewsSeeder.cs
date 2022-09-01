using GP.ECommerce1.Core.Application.Reviews.Commands.CreateReview;
using GP.ECommerce1.Core.Domain;
using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Infrastructure.DataSeeder.Seeders;

public class ReviewsSeeder
{
    private readonly IMediator _mediator;

    private static List<Review> Reviews { get; set; } = new();

    public ReviewsSeeder(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Seed(int count=1000)
    {
        Console.WriteLine("Seeding Reviews....");
        var products = await Task.Run(ProductsSeeder.GetAllProducts);
        var customers = await Task.Run(CustomersSeeder.GetAllCustomers);
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
            Console.WriteLine(i);
        }
        Console.WriteLine("Seeding Reviews Succeeded....");
    }
    
    public static void GenerateAndStoreAsJson(int count=1000)
    {
        Console.WriteLine("Generating Reviews....");
        var products = Task.Run(ProductsSeeder.GetAllProducts).Result;
        var customers = Task.Run(CustomersSeeder.GetAllCustomers).Result;
        List<Review> reviews = new();
        for (int i = 0; i < count; i++)
        {
            var customer = customers[Randoms.RandomInt(customers.Count)];
            var review = new Review()
            {
                Comment = Randoms.RandomSentence(30),
                Id = Guid.NewGuid(),
                Rate = Randoms.RandomRate(),
                CustomerId = customer.Id,
                ProductId = products[Randoms.RandomInt(products.Count)].Id,
                Date = Randoms.RandomDate(),
                CustomerName = $"{customer.FirstName} {customer.LastName}"
            };
            reviews.Add(review);
            Console.WriteLine(i);
        }

        Task.Run(() => FilesHelper.WriteToJsonFile("Reviews", reviews)).Wait();
        Reviews = reviews;
        Console.WriteLine("Seeding Reviews Succeeded....");
    }
    
    public static List<Review> GetAllDiscounts()
    {
        if (Reviews.Any())
            return Reviews;
        var commands = Task.Run(() => FilesHelper.ReadFromJsonFile<List<Review>>("Reviews")).Result;
        Reviews = commands;
        return commands;
    }
}