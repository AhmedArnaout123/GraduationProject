using AutoMapper;
using GP.ECommerce1.Core.Application.Categories.Commands.CreateCategory;
using GP.ECommerce1.Infrastructure.MongoDb.MongoEntities;
using GP.Utilix;
using MediatR;
using MongoDB.Driver;

namespace GP.ECommerce1.Infrastructure.MongoDb.Categories.CommandsHandlers.CreateCategory;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Result>
{
    private readonly IMapper _mapper;
    private readonly IMongoDatabase _database; 
    
    public CreateCategoryCommandHandler(IMongoClient client, IMapper mapper)
    {
        _mapper = mapper;
        _database = client.GetDatabase(Constants.GetDatabaseName());
    }
    
    public async Task<Result> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
            var result = new Result {IsSuccess = true};
            try
            {
                var collection = _database.GetCollection<Category>(Constants.CategoriesCollectionName);
                var category = _mapper.Map<Category>(request);

                await collection.InsertOneAsync(category, null, cancellationToken);
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