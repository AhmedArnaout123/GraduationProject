using AutoMapper;
using GP.ECommerce1.Core.Application.Orders.Commands.CreateOrder;
using GP.Utilix;
using MediatR;
using MongoDB.Driver;

namespace GP.ECommerce1.Infrastructure.MongoDb.Orders.CommandHandlers.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result>
{
    private readonly IMapper _mapper;
    private readonly IMongoDatabase _database; 
    
    public CreateOrderCommandHandler(IMongoClient client, IMapper mapper)
    {
        _mapper = mapper;
        _database = client.GetDatabase(Constants.DatabaseName);
    }
    
    public async Task<Result> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var result = new Result {IsSuccess = true};
        try
        {
            var collection = _database.GetCollection<MongoEntities.Order>(Constants.OrdersCollectionName);
            var order = _mapper.Map<MongoEntities.Order>(request);

            await collection.InsertOneAsync(order, null, cancellationToken);
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