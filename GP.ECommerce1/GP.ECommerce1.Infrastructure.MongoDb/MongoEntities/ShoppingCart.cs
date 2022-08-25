using MongoDB.Bson.Serialization.Attributes;

namespace GP.ECommerce1.Infrastructure.MongoDb.MongoEntities;

public class ShoppingCart
{
    [BsonId]
    public string CustomerId { get; set; } = "";
    
    public List<CartItem> Items { get; set; } = new();
}

public class CartItem
{
    public string ProductId { get; set; } = "";
    public string ProductName { get; set; } = "";
    public double ProductPrice { get; set; }
    public int Quantity { get; set; }
} 