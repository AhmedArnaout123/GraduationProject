using GP.ECommerce1.Core.Application.Testing;
using GP.ECommerce1.Infrastructure.DataSeeder;
using GP.ECommerce1.Infrastructure.DataSeeder.Seeders;
using GP.ECommerce1.Infrastructure.MongoDb.MongoEntities;
using GP.Utilix;
using MediatR;
using MongoDB.Driver;

namespace GP.ECommerce1.Infrastructure.MongoDb.Testing;

public class GetCategoryProductsTestingQueryHandler : IRequestHandler<GetCategoryProductsTestingQuery, TestingResult>
{
    private MongoClient _mongoClient;
    private IMongoDatabase _mongoDatabase;
    public GetCategoryProductsTestingQueryHandler()
    {
        _mongoClient = new MongoClient(Constants.ConnectionString);
        _mongoDatabase = _mongoClient.GetDatabase(Constants.GetDatabaseName());
    }
    
    public async Task<TestingResult> Handle(GetCategoryProductsTestingQuery request, CancellationToken cancellationToken)
    {
        var categories = await Task.Run(() => CategoriesSeeder.GetAllCategories(DataSeedingManager.CategoriesFileName));
        var collection = _mongoDatabase.GetCollection<Product>(Constants.ProductsCollectionName);
        var s = await _mongoDatabase.RunCommandAsync<string>("db.Products.getPlanCache.clear()");
        var r = await _mongoDatabase.RunCommandAsync<string>("db.Products.getPlanCach.clear()");
        return new TestingResult();
    }
}