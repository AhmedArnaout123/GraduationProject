using GP.Utilix;

namespace GP.ECommerce1.Core.Application.Products.Queries.GetCategoryProducts;

public class GetCategoryProductsQueryResponse
{
    public PaginationInfo? PaginationInfo { get; set; }
    public List<GetCategoryProductsQueryResponseEntry> Products { get; set; } = new();
}

public class GetCategoryProductsQueryResponseEntry
{
    public Guid Id { get; set; }

    public string Name { get; set; } = "";
    
    public double Price { get; set; } 
    
    public int DiscountPercentage { get; set; }

    public double DiscountValue => Price * DiscountPercentage / 100;
    
    public string MainImageUri { get; set; } = "";
}