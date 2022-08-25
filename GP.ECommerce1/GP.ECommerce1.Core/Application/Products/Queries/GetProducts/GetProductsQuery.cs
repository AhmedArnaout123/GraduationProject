using GP.ECommerce1.Core.Domain;
using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Core.Application.Products.Queries.GetProducts;

public class GetProductsQuery : IRequest<Result<List<Product>>>
{
    
}