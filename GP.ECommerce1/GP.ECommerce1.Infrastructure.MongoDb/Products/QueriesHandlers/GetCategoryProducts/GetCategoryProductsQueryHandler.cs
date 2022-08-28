using GP.ECommerce1.Core.Application.Products.Queries.GetCategoryProducts;
using GP.Utilix;
using MediatR;
using MongoDB.Driver;

namespace GP.ECommerce1.Infrastructure.MongoDb.Products.QueriesHandlers.GetCategoryProducts;

public class GetCategoryProductsQueryHandler : IRequestHandler<GetCategoryProductsQuery, Result<GetCategoryProductsQueryResponse>>
{
    private readonly IMongoDatabase _database;

    public GetCategoryProductsQueryHandler(IMongoClient client)
    {
        _database = client.GetDatabase(Constants.DatabaseName);
    }

    public async Task<Result<GetCategoryProductsQueryResponse>> Handle(GetCategoryProductsQuery request, CancellationToken cancellationToken)
    {
        var result = new Result<GetCategoryProductsQueryResponse> {IsSuccess = true};
        try
        {
            var collection = _database.GetCollection<MongoEntities.Product>(Constants.ProductsCollectionName);
            var filter =
                new FilterDefinitionBuilder<MongoEntities.Product>().Eq(p => p.CategoryId,
                    request.CategoryId);
            var products =  await collection.Find(filter).ToListAsync(cancellationToken);

            List<GetCategoryProductsQueryResponseEntry> entries = new();
            foreach (var product in products)
            {
                var entry = new GetCategoryProductsQueryResponseEntry
                {
                    DiscountPercentage = product.Discount?.Percentage ?? 0,
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    MainImageUri = product.MainImageUri
                };
                entries.Add(entry);
            }
            result.Value = new GetCategoryProductsQueryResponse
            {
                Products = entries
            };
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