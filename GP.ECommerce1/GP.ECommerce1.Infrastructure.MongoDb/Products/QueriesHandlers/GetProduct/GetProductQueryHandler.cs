using AutoMapper;
using GP.ECommerce1.Core.Application.Products.Queries.GetProduct;
using GP.ECommerce1.Core.Domain;
using GP.Utilix;
using MediatR;
using MongoDB.Driver;

namespace GP.ECommerce1.Infrastructure.MongoDb.Products.QueriesHandlers.GetProduct;

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, Result<GetProductQueryResponse>>
{
    private readonly IMapper _mapper;
    private readonly IMongoDatabase _database;

    public GetProductQueryHandler(IMongoClient client, IMapper mapper)
    {
        _mapper = mapper;
        _database = client.GetDatabase(Constants.GetDatabaseName());
    }

    public async Task<Result<GetProductQueryResponse>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var result = new Result<GetProductQueryResponse> {IsSuccess = true};
        try
        {
            var collection = _database.GetCollection<MongoEntities.Product>(Constants.ProductsCollectionName);
            var filter = new FilterDefinitionBuilder<MongoEntities.Product>().Eq(p => p.Id, request.ProductId);
            var productMongo =  await collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
            if (productMongo == null)
            {
                result.Error = "Product was not found.";
                result.IsSuccess = false;
            }
            else
            {
                var product = _mapper.Map<Product>(productMongo);
                result.Value = new GetProductQueryResponse
                {
                    Product = product,
                    LatestReviews = _mapper.Map<List<Review>>(productMongo.LatestReviews)
                };
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