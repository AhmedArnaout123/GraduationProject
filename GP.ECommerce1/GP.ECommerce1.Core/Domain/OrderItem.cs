namespace GP.ECommerce1.Core.Domain;

public class OrderItem
{
    public Guid ProductId { get; set; }
    public Guid OrderId { get; set; }
    public Guid? DiscountId { get; set; }
    public string ProductName { get; set; } = "";
    public double ProductPrice { get; set; }
    public double ProductSubtotal { get; set; }
    public int Quantity { get; set; }
}