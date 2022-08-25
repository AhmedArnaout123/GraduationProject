namespace GP.ECommerce1.Infrastructure.MongoDb.MongoEntities;

public class Category
{
    public string Id { get; set; } = "";

    public string Name { get; set; } = "";
    
    public string? ParentId { get; set; }
}