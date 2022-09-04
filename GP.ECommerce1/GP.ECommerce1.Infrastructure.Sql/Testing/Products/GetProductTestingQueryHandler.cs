using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using GP.ECommerce1.Core.Application.Testing.Products;
using GP.ECommerce1.Infrastructure.DataSeeder;
using GP.ECommerce1.Infrastructure.DataSeeder.Seeders;
using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Infrastructure.Sql.Testing.Products;

public class GetProductTestingQueryHandler : IRequestHandler<GetProductTestingQuery, TestingResult>
{
    private readonly SqlConnection _sqlConnection;

    public GetProductTestingQueryHandler(SqlConnection sqlConnection)
    {
        _sqlConnection = sqlConnection;
    }

    public async Task<TestingResult> Handle(GetProductTestingQuery request, CancellationToken cancellationToken)
    {
        var result = new TestingResult {IsSuccess = true};

        try
        {
            var products =
                await Task.Run(() => ProductsSeeder.GetAllProducts(DataSeedingManager.Products250000FileName));
            _sqlConnection.Open();
            for (int i = 0; i < request.TestsCount; i++)
            {
                var stmt = "DBCC DROPCLEANBUFFERS";
                var command = new SqlCommand(stmt, _sqlConnection);
                command.CommandTimeout = 10000;
                await command.ExecuteNonQueryAsync();
                stmt = "DBCC FREEPROCCACHE";
                command.CommandText = stmt;
                await command.ExecuteNonQueryAsync();
                stmt = "DBCC FREESYSTEMCACHE ('SQL Plans');";
                command.CommandText = stmt;
                await command.ExecuteNonQueryAsync();
                stmt =
                    @"SELECT Products.Id, Products.Name, Categories.Name as 'CategoryName', Discounts.Percentage, Discounts.Description as 'DiscountDescription', Products.Description as 'ProductDescription', MainImageUri, Price, CategoryId, DiscountId 
              FROM Products 
              INNER JOIN Categories on Categories.Id = Products.CategoryId
              LEFT Join Discounts on Discounts.Id = Products.DiscountId
              WHERE Products.Id=@ProductId";
                command.CommandText = stmt;
                
                var productId = products[Randoms.RandomInt(products.Count)].Id;
                command.Parameters.Add("@ProductId", SqlDbType.UniqueIdentifier);
                command.Parameters["@ProductId"].Value = productId;
                var stopWatch = new Stopwatch();
                stopWatch.Start();
                var reader = await command.ExecuteReaderAsync(cancellationToken);
                while (reader.Read())
                {
                }
                stopWatch.Stop();
                reader.Close();
                stmt = "SELECT Uri FROM ProductImages WHERE ProductId=@ProductId";
                command.CommandText = stmt;
                stopWatch.Start();
                var reader2 = await command.ExecuteReaderAsync(cancellationToken);
                while (reader2.Read())
                {
                }
                stopWatch.Stop();
                reader2.Close();
                stmt = @"SELECT Reviews.Id, Rate, Comment, Date, FirstName, LastName FROM Reviews 
                         INNER JOIN Customers on Customers.Id = Reviews.CustomerId
                         WHERE ProductId=@ProductId";
                command.CommandText = stmt;
                var customerIds = new List<object>();
                stopWatch.Start();
                var reader3 = await command.ExecuteReaderAsync(cancellationToken);
                while (reader3.Read())
                {
                    customerIds.Add(reader3["CustomerId"]);
                }
                stopWatch.Stop();
                reader3.Close();
                command.Parameters.Clear();
                command.Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier);
                command.CommandText = "SELECT FirstName, LastName FROM Customers WHERE Id=@CustomerId";
                var ids = customerIds.Select(value => Guid.Parse(Convert.ToString(value)!)).Distinct().ToList();
                foreach (var id in ids)
                {
                    command.Parameters["@CustomerId"].Value = id;
                    stopWatch.Start();
                    var reader4 = await command.ExecuteReaderAsync(cancellationToken);
                    while (reader4.Read())
                    {
                    }
                    stopWatch.Stop();
                    reader4.Close();
                }

                result.Millis.Add((int) stopWatch.ElapsedMilliseconds);
                command.Parameters.Clear();
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
            _sqlConnection.Close();
        }

        return result;
    }
}