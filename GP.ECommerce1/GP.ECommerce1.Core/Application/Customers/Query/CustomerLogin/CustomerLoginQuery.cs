using GP.ECommerce1.Core.Domain;
using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Core.Application.Customers.Query.CustomerLogin;

public class CustomerLoginQuery : IRequest<Result<List<Category>>>, IRequest<Result<Customer>>
{
    public string Email { get; set; } = "";

    public string Password { get; set; } = "";
}