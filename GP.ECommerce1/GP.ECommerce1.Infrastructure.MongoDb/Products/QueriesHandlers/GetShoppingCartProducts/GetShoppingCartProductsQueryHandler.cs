using AutoMapper;
using GP.ECommerce1.Core.Application.Products.GetShoppingCartProducts;
using GP.ECommerce1.Core.Domain;
using GP.Utilix;
using MediatR;
using MongoDB.Driver;

namespace GP.ECommerce1.Infrastructure.MongoDb.Products.QueriesHandlers.GetShoppingCartProducts;

public class GetShoppingCartProductsQueryHandler : IRequestHandler<GetShoppingCartProductsQuery, Result<ShoppingCart>>
{
    private readonly IMongoDatabase _database;
    private readonly IMapper _mapper;

    public GetShoppingCartProductsQueryHandler(IMongoClient client, IMapper mapper)
    {
        _mapper = mapper;
        _database = client.GetDatabase(Constants.GetDatabaseName());
    }

    public async Task<Result<ShoppingCart>> Handle(GetShoppingCartProductsQuery request, CancellationToken cancellationToken)
    {
        var result = new Result<ShoppingCart> {IsSuccess = true};
        try
        {
            var collection = _database.GetCollection<MongoEntities.ShoppingCart>(Constants.ShoppingCartsCollectionName);
            var filter =
                new FilterDefinitionBuilder<MongoEntities.ShoppingCart>().Eq(p => p.CustomerId, request.CustomerId);
            var cart =  await collection.Find(filter)
                .FirstOrDefaultAsync(cancellationToken);

            if (cart == null)
            {
                result.Error = "the Customer was not found";
                result.IsSuccess = false;
            }

            result.Value = _mapper.Map<ShoppingCart>(cart);
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