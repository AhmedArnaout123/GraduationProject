using AutoMapper;
using GP.ECommerce1.Core.Application.Customers.Commands.AddAddress;
using GP.ECommerce1.Infrastructure.MongoDb.MongoEntities;
using GP.Utilix;
using MediatR;
using MongoDB.Driver;

namespace GP.ECommerce1.Infrastructure.MongoDb.Customers.CommandsHandlers.AddAddress;

public class AddAddressCommandHandler : IRequestHandler<AddAddressCommand, Result>
{
    private readonly IMapper _mapper;
    private readonly IMongoDatabase _database; 
    
    public AddAddressCommandHandler(IMongoClient client, IMapper mapper)
    {
        _mapper = mapper;
        _database = client.GetDatabase(Constants.DatabaseName);
    }
    
    public async Task<Result> Handle(AddAddressCommand request, CancellationToken cancellationToken)
    {
        var result = new Result {IsSuccess = true};
        try
        {
            var collection = _database.GetCollection<Customer>(Constants.CustomersCollectionName);
            var address = _mapper.Map<Address>(request);

            var filter = new FilterDefinitionBuilder<Customer>().Eq(c => c.Id, request.CustomerId);
            var update = new UpdateDefinitionBuilder<Customer>().AddToSet(c => c.Addresses, address);
            await collection.UpdateOneAsync(filter, update, null,cancellationToken);
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