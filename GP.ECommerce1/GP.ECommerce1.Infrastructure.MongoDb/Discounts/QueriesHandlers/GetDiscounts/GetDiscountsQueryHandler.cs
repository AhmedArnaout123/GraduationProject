using AutoMapper;
using GP.ECommerce1.Core.Application.Discounts.Queries.GetDiscounts;
using GP.ECommerce1.Core.Domain;
using GP.Utilix;
using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GP.ECommerce1.Infrastructure.MongoDb.Discounts.QueriesHandlers.GetDiscounts;

public class GetDiscountsQueryHandler : IRequestHandler<GetDiscountsQuery, Result<List<Discount>>>
{
    private readonly IMapper _mapper;
    private readonly IMongoDatabase _database;

    public GetDiscountsQueryHandler(IMongoClient client, IMapper mapper)
    {
        _mapper = mapper;
        _database = client.GetDatabase(Constants.GetDatabaseName());
    }

    public async Task<Result<List<Discount>>> Handle(GetDiscountsQuery request, CancellationToken cancellationToken)
    {
        var result = new Result<List<Discount>> {IsSuccess = true};
        try
        {
            var collection = _database.GetCollection<MongoEntities.Discount>(Constants.DiscountsCollectionName);
            var discounts =  await collection.Find(new BsonDocument()).ToListAsync(cancellationToken);

            var c = _mapper.Map<List<Discount>>(discounts);
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