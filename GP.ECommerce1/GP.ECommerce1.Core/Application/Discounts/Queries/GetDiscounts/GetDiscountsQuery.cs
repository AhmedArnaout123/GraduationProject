using GP.ECommerce1.Core.Domain;
using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Core.Application.Discounts.Queries.GetDiscounts;

public class GetDiscountsQuery : IRequest<Result<List<Discount>>>
{
    
}