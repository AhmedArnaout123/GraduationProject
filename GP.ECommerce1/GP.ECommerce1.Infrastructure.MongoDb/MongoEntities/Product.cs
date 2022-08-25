namespace GP.ECommerce1.Infrastructure.MongoDb.MongoEntities;

public class Product
{
    public string Id { get; set; } = "";

    public Category Category { get; set; } = new();

    public Discount Discount { get; set; } = new();

    public string MainImageUri { get; set; } = "";

    public string Name { get; set; } = "";

    public string Description { get; set; } = "";

    public double Price { get; set; }
    
    public int RatesSum { get; set; }

    public List<string> Images { get; set; } = new();

    public List<Review> LatestReviews { get; set; } = new();
}