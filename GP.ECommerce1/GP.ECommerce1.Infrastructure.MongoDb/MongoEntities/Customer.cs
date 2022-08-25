namespace GP.ECommerce1.Infrastructure.MongoDb.MongoEntities;

public class Customer
{
    public string Id { get; set; } = "";
    
    public string FirstName { get; set; } = "";
    
    public string LastName { get; set; } = "";
    
    public string Email { get; set; } = "";

    public string PhoneNumber { get; set; } = "";
    
    public string Password { get; set; } = "";

    public List<Address> Addresses { get; set; }= new();
}

public class Address
{
    public string Country { get; set; } = "";
    public string State { get; set; } = "";
    public string City { get; set; } = "";
    public string Street1 { get; set; } = "";
    public string Street2 { get; set; } = "";
}