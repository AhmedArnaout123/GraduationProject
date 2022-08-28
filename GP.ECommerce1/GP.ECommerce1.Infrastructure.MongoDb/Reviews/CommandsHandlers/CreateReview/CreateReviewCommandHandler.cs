using AutoMapper;
using GP.ECommerce1.Core.Application.Reviews.Commands.CreateReview;
using GP.Utilix;
using MediatR;
using MongoDB.Driver;
using GP.ECommerce1.Infrastructure.MongoDb.MongoEntities;

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
            var collection1 = _database.GetCollection<MongoEntities.Review>(Constants.ReviewsCollectionName);
            var collection2 = _database.GetCollection<Product>(Constants.ProductsCollectionName);
            var review = _mapper.Map<MongoEntities.Review>(request);

            await collection1.InsertOneAsync(review, null,cancellationToken);

            var filter = new FilterDefinitionBuilder<MongoEntities.Product>().Eq(p => p.Id, request.ProductId);
            var product= await collection2.Find(filter).FirstOrDefaultAsync(cancellationToken);
            if (product.LatestReviews.Count >= 10)
            {
                var r = product.LatestReviews.OrderBy(r => r.Date).FirstOrDefault();    
                if(r != null)
                    product.LatestReviews.Remove(r);
            }
            product.LatestReviews.Add(review);
            var update =
                new UpdateDefinitionBuilder<MongoEntities.Product>().Set(p => p.LatestReviews, product.LatestReviews);
            await collection2.UpdateOneAsync(p => p.Id == product.Id, update, null, cancellationToken);
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