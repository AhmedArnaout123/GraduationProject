namespace GP.ECommerce1.Core.Domain;

public class Address
{
    public Guid Id { get; set; }
    
    public string Country { get; set; } = "";
    
    public string State { get; set; } = "";
    
    public string City { get; set; } = "";
    
    public string Street1 { get; set; } = "";
    
    public string Street2 { get; set; } = "";
    
    public Guid CustomerId { get; set; }
}