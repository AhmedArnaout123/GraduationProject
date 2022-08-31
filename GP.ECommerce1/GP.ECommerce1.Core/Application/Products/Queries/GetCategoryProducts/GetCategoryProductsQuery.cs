using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Core.Application.Products.Queries.GetCategoryProducts;

public class GetCategoryProductsQuery : IRequest<Result<GetCategoryProductsQueryResponse>>
{
    public PaginationParameters PaginationParameters { get; set; } = PaginationParameters.DefaultMaxParameters;
    
    public Guid CategoryId { get; set; }
}