using AutoMapper;
using GP.ECommerce1.Core.Application.Products.Commands.CreateProduct;
using GP.ECommerce1.Infrastructure.MongoDb.MongoEntities;
using GP.Utilix;
using MediatR;
using MongoDB.Driver;

namespace GP.ECommerce1.Infrastructure.MongoDb.Products.CommandHandlers.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result>
{
    private readonly IMapper _mapper;
    private readonly IMongoDatabase _database; 
    
    public CreateProductCommandHandler(IMongoClient client, IMapper mapper)
    {
        _mapper = mapper;
        _database = client.GetDatabase(Constants.GetDatabaseName());
    }
    
    public async Task<Result> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var result = new Result {IsSuccess = true};
        try
        {
            var collection = _database.GetCollection<Product>(Constants.ProductsCollectionName);
            var product = _mapper.Map<Product>(request);

            await collection.InsertOneAsync(product, null, cancellationToken);
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