using GP.ECommerce1.Core.Domain;
using GP.Utilix;
using JetBrains.Annotations;

namespace GP.ECommerce1.BlazorWebApp.Services.ClientServices;

public class CategoriesClientService : ClientServiceBase
{
    private const string DefaultPath = "Categories";
    public CategoriesClientService( HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<Result<List<Category>>> GetCategories()
    {
        var result = await SendRequest<List<Category>>(HttpMethod.Get, DefaultPath);
        return result;
    }
}