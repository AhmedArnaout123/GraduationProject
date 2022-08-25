namespace GP.ECommerce1.Infrastructure.MongoDb.MongoEntities;

public class Discount
{
    public string Id { get; set; } = "";

    public int Percentage { get; set; }

    public string Description { get; set; } = "";
}