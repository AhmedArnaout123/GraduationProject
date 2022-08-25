using GP.ECommerce1.Core.Domain;
using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Core.Application.Orders.Queries.GetOrdersStatuses;

public class GetOrderStatusesQuery : IRequest<Result<List<OrderStatus>>>
{
    
}