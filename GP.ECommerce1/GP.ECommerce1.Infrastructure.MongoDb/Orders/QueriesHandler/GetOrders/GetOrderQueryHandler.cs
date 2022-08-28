using AutoMapper;
using GP.ECommerce1.Core.Application.Orders.Queries.GetOrders;
using GP.ECommerce1.Core.Domain;
using GP.Utilix;
using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GP.ECommerce1.Infrastructure.MongoDb.Orders.QueriesHandler.GetOrders;

public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, Result<List<Order>>>
{
    private readonly IMapper _mapper;
    private readonly IMongoDatabase _database;

    public GetOrdersQueryHandler(IMongoClient client, IMapper mapper)
    {
        _mapper = mapper;
        _database = client.GetDatabase(Constants.DatabaseName);
    }

    public async Task<Result<List<Order>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var result = new Result<List<Order>> {IsSuccess = true};
        try
        {
            var collection = _database.GetCollection<MongoEntities.Order>(Constants.OrdersCollectionName);
            var orders =  await collection.Find(new BsonDocument()).ToListAsync(cancellationToken);

            var c = _mapper.Map<List<Order>>(orders);
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