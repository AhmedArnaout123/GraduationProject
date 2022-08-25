using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Core.Application.Customers.Commands.CreateCustomer;

public class CreateCustomerCommand : IRequest<Result>
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = "";
    
    public string LastName { get; set; } = "";
    
    public string PhoneNumber { get; set; } = "";
    
    public string Email { get; set; } = "";

    public string PasswordHash { get; set; } = "";
}