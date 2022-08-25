using AutoMapper;
using GP.ECommerce1.Core.Application.Customers.Query.CustomerLogin;
using GP.ECommerce1.Core.Domain;
using GP.Utilix;
using MediatR;
using MongoDB.Driver;

namespace GP.ECommerce1.Infrastructure.MongoDb.Customers.QueriesHandler.CustomerLogin;

public class CustomerLoginQueryHandler : IRequestHandler<CustomerLoginQuery, Result<Customer>>
{
    private readonly IMapper _mapper;
    private readonly IMongoDatabase _database; 
    
    public CustomerLoginQueryHandler(IMongoClient client, IMapper mapper)
    {
        _mapper = mapper;
        _database = client.GetDatabase(Constants.DatabaseName);
    }    
    
    public async Task<Result<Customer>> Handle(CustomerLoginQuery request, CancellationToken cancellationToken)
    {
        var result = new Result<Customer> {IsSuccess = true};
        try
        {
            var collection = _database.GetCollection<MongoEntities.Customer>(Constants.CustomersCollectionName);

            var filterBuilder = new FilterDefinitionBuilder<MongoEntities.Customer>();
            var emailFilter = filterBuilder.Eq(c => c.Email, request.Email);
            var passwordFilter = filterBuilder.Eq(c => c.Password, request.Password);
            var filter = filterBuilder.And(emailFilter, passwordFilter);

            var customer = await collection.Find(filter).FirstAsync();
            result.Value = _mapper.Map<Customer>(customer);
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