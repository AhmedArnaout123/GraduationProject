using System.Diagnostics;
using GP.ECommerce1.Core.Application.Testing.Products;
using GP.ECommerce1.Core.Domain;
using GP.ECommerce1.Infrastructure.DataSeeder;
using GP.ECommerce1.Infrastructure.DataSeeder.Seeders;
using GP.Utilix;
using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GP.ECommerce1.Infrastructure.MongoDb.Testing.Products;

public class GetCategoryProductsTestingQueryHandler : IRequestHandler<GetCategoryProductsTestingQuery, TestingResult>
{
    private MongoClient _mongoClient;
    private IMongoDatabase _mongoDatabase;

    public GetCategoryProductsTestingQueryHandler()
    {
        _mongoClient = new MongoClient(Constants.ConnectionString);
        _mongoDatabase = _mongoClient.GetDatabase(Constants.GetDatabaseName());
    }

    public async Task<TestingResult> Handle(GetCategoryProductsTestingQuery request,
        CancellationToken cancellationToken)
    {
        var categories = await Task.Run(() => CategoriesSeeder.GetAllCategories(DataSeedingManager.CategoriesFileName));
        var collection = _mongoDatabase.GetCollection<MongoEntities.Product>(Constants.ProductsCollectionName);
        List<int> testingResults = new();
        for (int i = 1; i <= request.TestsCount; i++)
        {
            var categoryId = categories[Randoms.RandomInt(categories.Count)].Id;
            var filter =
                new FilterDefinitionBuilder<MongoEntities.Product>().Eq("CategoryId", categoryId);
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var project = new ProjectionDefinitionBuilder<MongoEntities.Product>()
                .Include(d => d.Price)
                .Include(d => d.Name)
                .Include(d => d.Discount.Percentage)
                .Include(d => d.MainImageUri);
            var products = collection.Find(filter)
            .Project(project).ToCursor();
            foreach (var item in products.ToEnumerable())
            {
                
            }
            stopWatch.Stop();
            testingResults.Add(stopWatch.Elapsed.Milliseconds);
	    Console.WriteLine($"Finished Test {i}");
        }

        return new TestingResult{Millis = testingResults, ActionName = nameof(GetCategoryProductsTestingQuery)};
    }
}