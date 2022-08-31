using AutoMapper;
using GP.ECommerce1.Core.Application.Customers.Commands.CreateCustomer;
using GP.ECommerce1.Infrastructure.MongoDb.MongoEntities;
using GP.Utilix;
using MediatR;
using MongoDB.Driver;

namespace GP.ECommerce1.Infrastructure.MongoDb.Customers.CommandsHandlers.CreateCustomer;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Result>
{
    private readonly IMapper _mapper;
    private readonly IMongoDatabase _database; 
    
    public CreateCustomerCommandHandler(IMongoClient client, IMapper mapper)
    {
        _mapper = mapper;
        _database = client.GetDatabase(Constants.GetDatabaseName());
    }
    
    public async Task<Result> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var result = new Result {IsSuccess = true};
        try
        {
            var collection = _database.GetCollection<Customer>(Constants.CustomersCollectionName);
            var customer = _mapper.Map<Customer>(request);

            await collection.InsertOneAsync(customer, null, cancellationToken);
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