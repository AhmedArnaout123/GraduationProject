using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using GP.ECommerce1.Core.Application.Products.Queries.GetCategoryProducts;
using GP.ECommerce1.Core.Application.Testing;
using GP.ECommerce1.Core.Application.Testing.Products;
using GP.ECommerce1.Infrastructure.DataSeeder;
using GP.ECommerce1.Infrastructure.DataSeeder.Seeders;
using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Infrastructure.Sql.Testing;

public class GetCategoryProductsTestingQueryHandler : IRequestHandler<GetCategoryProductsTestingQuery, TestingResult>
{
    private readonly SqlConnection _connection;


    public GetCategoryProductsTestingQueryHandler(SqlConnection connection)
    {
        _connection = connection;
    }
    
    public async Task<TestingResult> Handle(GetCategoryProductsTestingQuery request, CancellationToken cancellationToken)
    {
        var categories = await Task.Run(() => CategoriesSeeder.GetAllCategories(DataSeedingManager.CategoriesFileName));
        var result = new TestingResult();
        string stmt = @"SELECT Products.Id, Price, Name, Products.MainImageUri, Discounts.Percentage as 'Discount'
        FROM Products
        INNER JOIN Discounts on Discounts.Id = Products.DiscountId
        Where CategoryId = @CategoryId";
        
        var command = new SqlCommand(stmt, _connection);
        command.Parameters.Add("@CategoryId", SqlDbType.UniqueIdentifier);
        List<GetCategoryProductsQueryResponseEntry> entries = new();
        try
        {
            _connection.Open();
            int x = 0;

            for (int i = 1; i <= request.TestsCount; i++)
            {
                var categoryId = categories[Randoms.RandomInt(categories.Count)].Id;
                command.Parameters["@CategoryId"].Value = categoryId;
                var stopWatch = new Stopwatch();
                var reader = await command.ExecuteReaderAsync(cancellationToken);
                stopWatch.Start();
                while (reader.Read())
                {
                }
                stopWatch.Stop();
                result.Millis.Add(stopWatch.Elapsed.Milliseconds);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            result.IsSuccess = false;
            result.Error = $"Exception: {ex.Message}";
        }
        finally
        {
            _connection.Close();
        }

        return result;
    }
}