using System.Diagnostics;
using GP.ECommerce1.Core.Application.Products.Commands.CreateProduct;
using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Infrastructure.DataSeeder.Seeders;

public class ProductsSeeder
{
    private readonly IMediator _mediator;
    private readonly DataSeedingHelper _dataSeedingHelper;

    public ProductsSeeder(IMediator mediator, DataSeedingHelper dataSeedingHelper)
    {
        _mediator = mediator;
        _dataSeedingHelper = dataSeedingHelper;
        GenerateInitialData();
    }

    public async Task Seed()
    {
        Console.WriteLine("Seeding Products....");
        var categories = await _dataSeedingHelper.GetAllCategories();
        var discounts = await _dataSeedingHelper.GetAllDiscounts();

        var stopWatch = new Stopwatch();
        stopWatch.Start();
        for (int i = 1; i <= 1000; i++)
        {
            var imagesUri = new List<string>();
            for(int j= 1; j <= Randoms.RandomInt(0, 8); j++)
            {
                imagesUri.Add(_defaultImageUrl);
            }
            var command = new CreateProductCommand
            {
                Id = Guid.NewGuid(),
                CategoryId = categories[Randoms.RandomInt(categories.Count)].Id,
                DiscountId = discounts[Randoms.RandomInt(discounts.Count)].Id,
                Description = _productsDescriptions[Randoms.RandomInt(_productsDescriptions.Length)],
                Name = _productsNames[Randoms.RandomInt(_productsNames.Length)],
                Price = Randoms.RandomPrice(),
                MainImageUri = _defaultImageUrl,
                Images = imagesUri
            };
            await _mediator.Send(command);
            Console.WriteLine(i);
        }   
        stopWatch.Stop();
        Console.WriteLine("Seeding Products Succeeded");
        Console.WriteLine($"Times Consumed: {stopWatch}");
    }
    
    private string[] _productsNames = new String[100];
    private string[] _productsDescriptions = new string[100];
    private string _defaultImageUrl = "https://www.themelocation.com/wp-content/uploads/2015/01/woocommerce113.jpg";

    void GenerateInitialData()
    {
        for (int i = 0; i < _productsNames.Length; i++)
        {
            _productsNames[i] = Randoms.RandomSentence(Randoms.RandomInt(2, 10));
        }
        
        for (int i = 0; i < _productsDescriptions.Length; i++)
        {
            _productsDescriptions[i] = Randoms.RandomDescription();
        }
    }
}