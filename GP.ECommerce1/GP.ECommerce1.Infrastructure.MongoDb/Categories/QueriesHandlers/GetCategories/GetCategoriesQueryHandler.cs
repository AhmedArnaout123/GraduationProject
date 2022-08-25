using AutoMapper;
using GP.ECommerce1.Core.Application.Categories.Queries.GetCategories;
using GP.ECommerce1.Core.Domain;
using GP.Utilix;
using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GP.ECommerce1.Infrastructure.MongoDb.Categories.QueriesHandlers.GetCategories;

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, Result<List<Category>>>
{
    private readonly IMapper _mapper;
    private readonly IMongoDatabase _database;

    public GetCategoriesQueryHandler(IMongoClient client, IMapper mapper)
    {
        _mapper = mapper;
        _database = client.GetDatabase(Constants.DatabaseName);
    }

    public async Task<Result<List<Category>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var result = new Result<List<Category>> {IsSuccess = true};
        try
        {
            var collection = _database.GetCollection<MongoEntities.Category>(Constants.CategoriesCollectionName);
            var categories =  await collection.Find(new BsonDocument()).ToListAsync(cancellationToken);

            var c = _mapper.Map<List<Category>>(categories);
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