using GP.ECommerce1.Core.Application.Discounts.Commands.CreateDiscount;
using GP.ECommerce1.Core.Domain;
using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Infrastructure.DataSeeder.Seeders;

public class DiscountsSeeder
{
    private readonly IMediator _mediator;

    private static List<Discount> Discounts { get; set; } = new();

    public DiscountsSeeder(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Seed(string fileName="Discounts")
    {
        Console.WriteLine("Seeding Discounts....");
        List<Discount> discounts = GetAllDiscounts(fileName);
        foreach (var discount in discounts)
        {
            var command = new CreateDiscountCommand
            {
                Id = discount.Id,
                Percentage = discount.Percentage,
                Description = discount.Description
            };
            await _mediator.Send(command);
        }
        Console.WriteLine("Seeding Discounts Succeeded....");
    }

    public static void GenerateAndStoreAsJson(string fileName="Discounts")
    {
        Console.WriteLine("Generating Discounts....");
        var percentages = new [] {10, 15, 20, 25, 30, 35, 40, 50, 60, 70};
        List<Discount> discounts = new();
        foreach (var percentage in percentages)
        {
            var command = new Discount
            {
                Id = Guid.NewGuid(),
                Percentage = percentage,
                Description = Randoms.RandomSentence(6)
            };
            discounts.Add(command);
        }

        Task.Run(() => FilesHelper.WriteToJsonFile(fileName, discounts)).Wait();
        Discounts = discounts;
        Console.WriteLine("Generating Discounts Succeeded....");
    }
    
    public static List<Discount> GetAllDiscounts(string fileName="Discount")
    {
        if (Discounts.Any())
            return Discounts;
        var commands = Task.Run(() => FilesHelper.ReadFromJsonFile<List<Discount>>(fileName)).Result;
        Discounts = commands;
        return commands;
    }
}