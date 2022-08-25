namespace GP.ECommerce1.Infrastructure.MongoDb.MongoEntities;

public class Review
{
    public string Id { get; set; } = "";
    
    public string ProductId { get; set; } = "";
    
    public string CustomerId { get; set; } = "";
    
    public int Rate { get; set; }
    
    public string Comment { get; set; } = "";
}