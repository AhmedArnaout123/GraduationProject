using GP.ECommerce1.Core.Application.Products.Queries.GetCategoryProducts;
using GP.Utilix;
using JetBrains.Annotations;

namespace GP.ECommerce1.BlazorWebApp.Services.ClientServices;

public class ProductsClientService : ClientServiceBase
{
    private const string DefaultPath = "Products";
    public ProductsClientService([NotNull] HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<Result<GetCategoryProductsQueryResponse>> GetCategoryProducts(GetCategoryProductsQuery query)
    {
        var result = await SendRequest<GetCategoryProductsQueryResponse>(HttpMethod.Get,
            $"{DefaultPath}/CategoryProducts?{nameof(GetCategoryProductsQuery.CategoryId)}={query.CategoryId}");
        return result;
    }
}