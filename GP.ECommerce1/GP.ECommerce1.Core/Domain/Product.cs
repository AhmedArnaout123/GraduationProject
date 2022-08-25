namespace GP.ECommerce1.Core.Domain;

public class Product
{
    public Guid Id { get; set; }
    
    public Guid CategoryId { get; set; }

    public string CategoryName { get; set; } = "";
    
    public Guid? DiscountId { get; set; }
    
    public double? DiscountPercentage { get; set; }

    public string MainImageUri { get; set; } = "";

    public string Name { get; set; } = "";

    public string Description { get; set; } = "";

    public double Price { get; set; }
    
    public int RatesSum { get; set; }

    public List<string> Images { get; set; } = new();
}