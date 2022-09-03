using GP.ECommerce1.Core.Domain;
using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Core.Application.Products.GetShoppingCartProducts;

public class GetShoppingCartProductsQuery : IRequest<Result<ShoppingCart>>
{
    public Guid CustomerId { get; set; }
}