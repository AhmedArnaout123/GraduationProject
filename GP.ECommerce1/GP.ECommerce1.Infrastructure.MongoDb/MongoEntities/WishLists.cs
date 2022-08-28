using MongoDB.Bson.Serialization.Attributes;

namespace GP.ECommerce1.Infrastructure.MongoDb.MongoEntities;

public class WishList
{
    [BsonId]
    public Guid CustomerId { get; set; }
    
    public List<WishListItem> Items { get; set; } = new();
}

public class WishListItem
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = "";
    
    public string ProductMainImageUri { get; set; } = "";
    
    public double ProductPrice { get; set; }
}