using System.Diagnostics;
using AutoMapper;
using GP.ECommerce1.Core.Application.Products.Queries.GetProduct;
using GP.ECommerce1.Core.Application.Testing.Products;
using GP.ECommerce1.Core.Domain;
using GP.ECommerce1.Infrastructure.DataSeeder;
using GP.ECommerce1.Infrastructure.DataSeeder.Seeders;
using GP.Utilix;
using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GP.ECommerce1.Infrastructure.MongoDb.Testing.Products;

public class GetProductTestingQueryHandler : IRequestHandler<GetProductTestingQuery,TestingResult>
{
    private readonly IMapper _mapper;
    private readonly IMongoDatabase _database;

    public GetProductTestingQueryHandler(IMongoClient client, IMapper mapper)
    {
        _mapper = mapper;
        _database = client.GetDatabase(Constants.GetDatabaseName());
    }
    
    public async Task<TestingResult> Handle(GetProductTestingQuery request, CancellationToken cancellationToken)
    {
        var result = new TestingResult() {IsSuccess = true};
        try
        {
            var products =
                await Task.Run(() => ProductsSeeder.GetAllProducts(DataSeedingManager.Products250000FileName));
            var collection = _database.GetCollection<BsonDocument>(Constants.ProductsCollectionName);
            for (int i = 0; i < request.TestsCount; i++)
            {
                var productId = products[Randoms.RandomInt(products.Count)].Id;
                var filter = new FilterDefinitionBuilder<BsonDocument>().Eq("Id", productId);
                var productMongo = collection.Find(filter);
                var stopWatch = new Stopwatch();
                stopWatch.Start();
                var p = collection.Find(filter).ToCursor();
                foreach (var item in p.ToEnumerable())
                {
                
                }
                stopWatch.Stop();
                result.Millis.Add(stopWatch.Elapsed.Milliseconds);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            result.IsSuccess = false;
            result.Error = e.Message;
        }

        return result;
    }
}