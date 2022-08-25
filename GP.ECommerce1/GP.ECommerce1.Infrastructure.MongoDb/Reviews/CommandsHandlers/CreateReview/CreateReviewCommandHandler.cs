using AutoMapper;
using GP.ECommerce1.Core.Application.Reviews.Commands.CreateReview;
using GP.ECommerce1.Core.Domain;
using GP.Utilix;
using MediatR;
using MongoDB.Driver;

namespace GP.ECommerce1.Infrastructure.MongoDb.Reviews.CommandsHandlers.CreateReview;

public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, Result>
{
    private readonly IMapper _mapper;
    private readonly IMongoDatabase _database; 
    
    public CreateReviewCommandHandler(IMongoClient client, IMapper mapper)
    {
        _mapper = mapper;
        _database = client.GetDatabase(Constants.DatabaseName);
    }
    
    public async Task<Result> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var result = new Result {IsSuccess = true};
        try
        {
            var collection1 = _database.GetCollection<Review>(Constants.ReviewsCollectionName);
            var collection2 = _database.GetCollection<Customer>(Constants.CustomersCollectionName);
            var review = _mapper.Map<Review>(request);

            await collection1.InsertOneAsync(review, null, cancellationToken);
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