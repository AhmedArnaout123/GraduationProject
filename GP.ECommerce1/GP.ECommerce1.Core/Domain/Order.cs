namespace GP.ECommerce1.Core.Domain;

public class Order
{
    public Guid Id { get; set; }
    
    public DateTime Date { get; set; }

    public double Subtotal => Items.Sum(i => i.ProductSubtotal);
    
    public Guid CustomerId { get; set; }

    public string CustomerName { get; set; } = "";

    public Address Address { get; set; } = new();
    
    public List<OrderItem> Items { get; set; } = new();

    public OrderStatus Status { get; set; } = OrderStatus.AwaitingConfirmation;
}

public enum OrderStatus
{
    Delivered,
    AwaitingConfirmation,
    Shipping
}

public static class OrderStatusExtensions
{
    public static string ToText(this OrderStatus s)
    {
        return s switch
        {
            OrderStatus.Delivered => "Delivered",
            OrderStatus.Shipping => "Shipping",
            _ => "Awaiting Conformation"
        };
    }

    public static OrderStatus ToOrderStatus(this string s)
    {
        return s switch
        {
            "Delivered" => OrderStatus.Delivered,
            "Shipping" => OrderStatus.Shipping,
            _ => OrderStatus.AwaitingConfirmation
        };
    }
}

public class OrderItem
{
    public Guid ProductId { get; set; }
    
    public Guid OrderId { get; set; }
    
    public Discount? Discount { get; set; }
    
    public string ProductName { get; set; } = "";
    
    public double ProductPrice { get; set; }

    public double ProductSubtotal =>
        Discount == null ? ProductPrice : ProductPrice - ProductPrice * Discount.Percentage / 100;
    
    public int Quantity { get; set; }
}