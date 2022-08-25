namespace GP.ECommerce1.Core.Domain;

public class Order
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public double Subtotal { get; set; }
    public Guid CustomerId { get; set; }
    public Guid StatusId { get; set; }
    
    public string Status { get; set; }

    public List<OrderItem> Items { get; set; } = new();
}