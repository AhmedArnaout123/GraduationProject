using GP.ECommerce1.Core.Domain;
using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Core.Application.Orders.Queries.GetCustomerOrders;

public class GetCustomerOrdersQuery : IRequest<Result<List<Order>>>
{
    public Guid CustomerId { get; set; }
}