using GP.ECommerce1.Core.Application.Customers.Commands.AddProductToWishList;
using GP.ECommerce1.Infrastructure.MongoDb.MongoEntities;
using GP.Utilix;
using MediatR;
using MongoDB.Driver;

namespace GP.ECommerce1.Infrastructure.MongoDb.Customers.CommandsHandlers.AddProductToWishList;

public class AddProductToWishListCommandHandler : IRequestHandler<AddProductToWishListCommand, Result>
{
    private readonly IMongoDatabase _database; 
    
    public AddProductToWishListCommandHandler(IMongoClient client)
    {
        _database = client.GetDatabase(Constants.GetDatabaseName());
    }
    
    public async Task<Result> Handle(AddProductToWishListCommand request, CancellationToken cancellationToken)
    {
        var result = new Result {IsSuccess = true};
        try
        {
            var collection = _database.GetCollection<WishList>(Constants.WishListsCollectionName);
            var item = new WishListItem
            {
                ProductId = request.ProductId,
                ProductName = request.ProductName,
                ProductPrice = request.ProductPrice
            };

            var filter =
                new FilterDefinitionBuilder<WishList>().Eq(c => c.CustomerId, request.CustomerId);
            var update = new UpdateDefinitionBuilder<WishList>().AddToSet(c => c.Items, item);
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