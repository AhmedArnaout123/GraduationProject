using AutoMapper;
using GP.ECommerce1.Core.Application.Products.Queries.GetProducts;
using GP.ECommerce1.Core.Domain;
using GP.Utilix;
using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GP.ECommerce1.Infrastructure.MongoDb.Products.QueriesHandlers.GetProducts;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, Result<List<Product>>>
{
    private readonly IMapper _mapper;
    private readonly IMongoDatabase _database;

    public GetProductsQueryHandler(IMongoClient client, IMapper mapper)
    {
        _mapper = mapper;
        _database = client.GetDatabase(Constants.GetDatabaseName());
    }

    public async Task<Result<List<Product>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var result = new Result<List<Product>> {IsSuccess = true};
        try
        {
            var collection = _database.GetCollection<MongoEntities.Product>(Constants.ProductsCollectionName);
            var products =  await collection.Find(new BsonDocument()).ToListAsync(cancellationToken);

            var c = _mapper.Map<List<Product>>(products);
            result.Value = c;
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