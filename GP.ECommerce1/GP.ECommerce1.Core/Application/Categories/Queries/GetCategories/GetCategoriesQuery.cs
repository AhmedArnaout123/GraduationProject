using GP.ECommerce1.Core.Domain;
using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Core.Application.Categories.Queries.GetCategories;

public class GetCategoriesQuery : IRequest<Result<List<Category>>>
{
        
}