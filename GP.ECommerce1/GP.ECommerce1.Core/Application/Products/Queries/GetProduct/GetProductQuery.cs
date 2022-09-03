using GP.ECommerce1.Core.Domain;
using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Core.Application.Products.Queries.GetProduct;

public class GetProductQuery : IRequest<Result<GetProductQueryResponse>>
{
    public Guid ProductId { get; set; }
}