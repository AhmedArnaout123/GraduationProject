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

    public async Task Seed(string fileName)
    {
        Console.WriteLine("Seeding Reviews....");
        var reviews = GetAllReviews(fileName);
        foreach (var review in reviews)
        {
            var command = new CreateReviewCommand
            {
                Comment = review.Comment,
                Id = review.Id,
                Rate = review.Rate,
                CustomerId = review.CustomerId,
                ProductId = review.ProductId,
                Date = review.Date,
                CustomerName = review.CustomerName
            };
            await _mediator.Send(command);
        }
        Console.WriteLine("Seeding Reviews Succeeded....");
    }
    
    public static void GenerateAndStoreAsJson(string fileName, string customersFileName, string productsFileName, int count)
    {
        Console.WriteLine("Generating Reviews....");
        var products = Task.Run(() => ProductsSeeder.GetAllProducts(productsFileName)).Result;
        var customers = Task.Run(() => CustomersSeeder.GetAllCustomers(customersFileName)).Result;
        List<Review> reviews = new();
        for (int i = 0; i < count; i++)
        {
            var customer = customers[Randoms.RandomInt(customers.Count)];
            var review = new Review
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

        Task.Run(() => FilesHelper.WriteToJsonFile(fileName, reviews)).Wait();
        Reviews = reviews;
        Console.WriteLine("Seeding Reviews Succeeded....");
    }
    
    public static List<Review> GetAllReviews(string fileName)
    {
        if (Reviews.Any())
            return Reviews;
        var commands = Task.Run(() => FilesHelper.ReadFromJsonFile<List<Review>>(fileName)).Result;
        Reviews = commands;
        return commands;
    }
}