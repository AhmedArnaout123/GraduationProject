namespace GP.ECommerce1.Core.Application.Products.Queries.GetCategoryProducts;

public class GetCategoryProductsQueryResponse
{
    public List<GetCategoryProductsQueryResponseEntry> Products { get; set; } = new();
}

public class GetCategoryProductsQueryResponseEntry
{
    public Guid Id { get; set; }

    public string Name { get; set; } = "";
    
    public double Price { get; set; } 
    
    public double Discount { get; set; }

    public string MainImageUri { get; set; } = "";
}