using GP.ECommerce1.Core.Domain;
using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Core.Application.Products.Commands.CreateProduct;

public class CreateProductCommand : IRequest<Result>
{
    public Guid Id { get; set; }

    public Guid CategoryId { get; set; }

    public string CategoryName { get; set; } = "";

    public Guid? CategoryParentId { get; set; }

    public Discount? Discount { get; set; }
    
    public string Name { get; set; } = "";
    
    public string Description { get; set; } = "";
    
    public double Price { get; set; }
    
    public string MainImageUri { get; set; } = "";

    public List<string> Images { get; set; } = new();
}