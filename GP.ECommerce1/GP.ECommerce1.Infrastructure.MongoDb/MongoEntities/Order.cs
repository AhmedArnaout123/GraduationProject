using GP.ECommerce1.Core.Domain;

namespace GP.ECommerce1.Infrastructure.MongoDb.MongoEntities;

public class Order
{
    public string Id { get; set; } = "";
    
    public DateTime Date { get; set; }
    
    public double Subtotal { get; set; }

    public string CustomerId { get; set; } = "";

    public string CustomerName { get; set; } = "";

    public Address Address { get; set; } = new();
    
    public List<OrderItem> Items { get; set; } = new();

    public OrderStatus Status { get; set; } = OrderStatus.AwaitingConfirmation;
}

public class OrderItem
{
    public Guid ProductId { get; set; }
    
    public Guid OrderId { get; set; }
    
    public Discount? Discount { get; set; }
    
    public string ProductName { get; set; } = "";
    
    public double ProductPrice { get; set; }
    
    public double ProductSubtotal { get; set; }
    
    public int Quantity { get; set; }
}