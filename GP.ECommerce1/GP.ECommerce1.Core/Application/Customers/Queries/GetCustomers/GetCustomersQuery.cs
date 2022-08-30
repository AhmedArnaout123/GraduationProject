using GP.ECommerce1.Core.Domain;
using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Core.Application.Customers.Query.GetCustomers;

public class GetCustomersQuery : IRequest<Result<List<Customer>>>
{
    
}