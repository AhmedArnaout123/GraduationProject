using AutoMapper;
using GP.ECommerce1.Core.Application.Customers.Query.GetCustomers;
using GP.ECommerce1.Core.Domain;
using GP.Utilix;
using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GP.ECommerce1.Infrastructure.MongoDb.Customers.QueriesHandler.GetCustomers;

public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, Result<List<Customer>>>
{
    private readonly IMapper _mapper;
    private readonly IMongoDatabase _database; 
    
    public GetCustomersQueryHandler(IMongoClient client, IMapper mapper)
    {
        _mapper = mapper;
        _database = client.GetDatabase(Constants.DatabaseName);
    }    
    
    public async Task<Result<List<Customer>>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        var result = new Result<List<Customer>> {IsSuccess = true};
        try
        {
            var collection = _database.GetCollection<MongoEntities.Customer>(Constants.CustomersCollectionName);
            
            var customer = await collection.Find(new BsonDocument()).ToListAsync(cancellationToken);
            result.Value = _mapper.Map<List<Customer>>(customer);
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