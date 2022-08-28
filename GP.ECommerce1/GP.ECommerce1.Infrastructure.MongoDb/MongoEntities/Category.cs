namespace GP.ECommerce1.Infrastructure.MongoDb.MongoEntities;

public class Category
{
    public Guid Id { get; set; }

    public string Name { get; set; } = "";
    
    public Guid? ParentId { get; set; }
}