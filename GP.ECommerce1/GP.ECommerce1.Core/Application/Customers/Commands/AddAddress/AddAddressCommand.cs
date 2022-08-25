using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Core.Application.Customers.Commands.AddAddress;

public class AddAddressCommand : IRequest<Result>
{
    public Guid Id { get; set; }

    public string Country { get; set; } = "";
    
    public string State { get; set; } = "";
    
    public string City { get; set; } = "";
    
    public string Street1 { get; set; } = "";

    public string Street2 { get; set; } = "";
    
    public Guid CustomerId { get; set; }
}