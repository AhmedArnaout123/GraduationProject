using GP.ECommerce1.BlazorWebApp.Models;
using GP.ECommerce1.Core.Application.Testing.Products;
using GP.Utilix;
using JetBrains.Annotations;

namespace GP.ECommerce1.BlazorWebApp.Services.ClientServices;

public class DbTestingClientService
{
    private const string DefaultPath = "DbTesting";

    private HttpClient _client;
    private string Host { get; set; } = "https://localhost:7002";

    public DbTestingClientService(IHttpClientFactory clientFactory)
    {
        _client = clientFactory.CreateClient("Db Testing Client");
        _client.Timeout = Timeout.InfiniteTimeSpan;
    }

    private void SetHost(DbType dbType)
    {
        Host = dbType is DbType.MongoDb ? "https://localhost:7002" : "https://localhost:8002";
    }

    public async Task<TestingResult> GetCategoryProducts(int count, DbType dbType)
    {
        SetHost(dbType);
        var result = await _client.GetFromJsonAsync<TestingResult>($"{Host}/{DefaultPath}/GetCategoryProducts?TestsCount={count}");
        return result!;
    }
    
    public async Task<TestingResult> GetProduct(int count, DbType dbType)
    {
        SetHost(dbType);
        var result = await _client.GetFromJsonAsync<TestingResult>($"{Host}/{DefaultPath}/GetProduct?TestsCount={count}");
        return result!;
    }
    
    public async Task<TestingResult> GetAwaitingConfirmationOrders(int count, DbType dbType)
    {
        SetHost(dbType);
        var result = await _client.GetFromJsonAsync<TestingResult>($"{Host}/{DefaultPath}/AwaitingConfirmationOrders?TestsCount={count}");
        return result!;
    }
}