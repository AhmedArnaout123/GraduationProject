using GP.ECommerce1.Core.Domain;

namespace GP.ECommerce1.Core.Application.Products.Queries.GetProduct;

public class GetProductQueryResponse
{
    public Product Product { get; set; } = new();

    public List<Review> LatestReviews { get; set; } = new();
}