using GP.ECommerce1.Core.Application.Customers.Commands.AddProductToShoppingCart;
using GP.ECommerce1.Infrastructure.MongoDb.MongoEntities;
using GP.Utilix;
using MediatR;
using MongoDB.Driver;

namespace GP.ECommerce1.Infrastructure.MongoDb.Customers.CommandsHandlers.AddProductToShoppingCart;

public class AddProductToShoppingCartCommandHandler : IRequestHandler<AddProductToShoppingCartCommand, Result>
{
    private readonly IMongoDatabase _database; 
    
    public AddProductToShoppingCartCommandHandler(IMongoClient client)
    {
        _database = client.GetDatabase(Constants.DatabaseName);
    }
    
    public async Task<Result> Handle(AddProductToShoppingCartCommand request, CancellationToken cancellationToken)
    {
        var result = new Result {IsSuccess = true};
        try
        {
            var collection = _database.GetCollection<ShoppingCart>(Constants.ShoppingCartsCollectionName);
            var item = new CartItem
            {
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                ProductName = request.ProductName,
                ProductPrice = request.ProductPrice
            };

            var filter =
                new FilterDefinitionBuilder<ShoppingCart>().Eq(c => c.CustomerId, request.CustomerId);
            var update = new UpdateDefinitionBuilder<ShoppingCart>().AddToSet(c => c.Items, item);
            await collection.UpdateOneAsync(filter, update, new UpdateOptions{IsUpsert = true}, cancellationToken);
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