using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Core.Application.Customers.Commands.AddProductToWishList;

public class AddProductToWishListCommand : IRequest<Result>
{
    public Guid CustomerId { get; set; }
    
    public Guid ProductId { get; set; }
    
    public string ProductName { get; set; } = "";
    
    public double ProductPrice { get; set; }
}