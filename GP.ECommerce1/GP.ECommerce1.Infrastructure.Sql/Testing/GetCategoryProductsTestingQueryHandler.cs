using System.Data.SqlClient;
using System.Diagnostics;
using GP.ECommerce1.Core.Application.Products.Queries.GetCategoryProducts;
using GP.ECommerce1.Core.Application.Testing;
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
        var result = new TestingResult();
        string stmt = @"SELECT Products.Id, Price, Name, Products.MainImageUri, Discounts.Percentage as 'Discount'
        FROM Products
        INNER JOIN Discounts on Discounts.Id = Products.DiscountId
        Where CategoryId = @CategoryId";
        
        var command = new SqlCommand(stmt, _connection);
        // command.Parameters.AddWithValue("@CategoryId", request.CategoryId);
        List<GetCategoryProductsQueryResponseEntry> entries = new();
        try
        {
            _connection.Open();
            var stopWatch = new Stopwatch();
            int x = 0;
            stopWatch.Start();
            var reader = await command.ExecuteReaderAsync(cancellationToken);

            while (reader.Read())
            {
            }
            
            stopWatch.Stop();
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