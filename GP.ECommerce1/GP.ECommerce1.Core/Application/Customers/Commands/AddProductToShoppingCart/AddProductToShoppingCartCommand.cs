using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Core.Application.Customers.Commands.AddProductToShoppingCart;

public class AddProductToShoppingCartCommand : IRequest<Result>
{
    public Guid CustomerId { get; set; }
    
    public Guid ProductId { get; set; }
    
    public int Quantity { get; set; }

    public string ProductName { get; set; } = "";
    
    public double ProductPrice { get; set; }
}