using System.Diagnostics;
using AutoMapper;
using GP.ECommerce1.Core.Application.Testing.Orders;
using GP.ECommerce1.Core.Domain;
using GP.Utilix;
using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GP.ECommerce1.Infrastructure.MongoDb.Testing.Orders;

public class GetAwaitingConfirmationOrdersTestingQueryHandler : IRequestHandler<GetAwaitingConfirmationOrdersTestingQuery, TestingResult>
{
    private readonly IMapper _mapper;
    private readonly IMongoDatabase _database;

    public GetAwaitingConfirmationOrdersTestingQueryHandler(IMongoClient client, IMapper mapper)
    {
        _mapper = mapper;
        _database = client.GetDatabase(Constants.GetDatabaseName());
    }
    public async Task<TestingResult> Handle(GetAwaitingConfirmationOrdersTestingQuery request, CancellationToken cancellationToken)
    {
        var result = new TestingResult {IsSuccess = true};
        try
        {
            var collection = _database.GetCollection<BsonDocument>(Constants.OrdersCollectionName);
            for (int i = 0; i < request.TestsCount; i++)
            {
                var filter = new FilterDefinitionBuilder<BsonDocument>().Eq("Status", OrderStatus.AwaitingConfirmation);
                var stopWatch = new Stopwatch();
                stopWatch.Start();
                collection.Find(filter).ToList();
                stopWatch.Stop();
                result.Millis.Add(stopWatch.Elapsed.Milliseconds);
                Console.WriteLine($"Finished Running Test {i}");
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