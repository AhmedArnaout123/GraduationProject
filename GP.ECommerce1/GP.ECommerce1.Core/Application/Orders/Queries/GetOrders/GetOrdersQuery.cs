using GP.ECommerce1.Core.Domain;
using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Core.Application.Orders.Queries.GetOrders;

public class GetOrdersQuery : IRequest<Result<List<Order>>>
{
    
}