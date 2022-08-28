namespace GP.ECommerce1.Core.Domain;

public class Customer
{
    public Guid Id { get; set; }
    
    public string FirstName { get; set; } = "";
    
    public string LastName { get; set; } = "";
    
    public string Email { get; set; } = "";

    public string PhoneNumber { get; set; } = "";

    public List<Address> Addresses { get; set; } = new();
}