using System.Data.SqlClient;
using System.Diagnostics;
using GP.ECommerce1.Core.Application.Testing.Orders;
using GP.ECommerce1.Infrastructure.DataSeeder;
using GP.ECommerce1.Infrastructure.DataSeeder.Seeders;
using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Infrastructure.Sql.Testing.Orders;

public class GetAwaitingConfirmationOrdersTestingQueryHandler : IRequestHandler<GetAwaitingConfirmationOrdersTestingQuery, TestingResult>
{
    private readonly SqlConnection _connection;


    public GetAwaitingConfirmationOrdersTestingQueryHandler(SqlConnection connection)
    {
        _connection = connection;
    }
    public async Task<TestingResult> Handle(GetAwaitingConfirmationOrdersTestingQuery request, CancellationToken cancellationToken)
    {
        var result = new TestingResult {IsSuccess = false};
        try
        {
            _connection.Open();
            var stmt = "DBCC DROPCLEANBUFFERS";
            var command = new SqlCommand(stmt, _connection);
            command.CommandTimeout = 1000000;
            await command.ExecuteNonQueryAsync();
            stmt = "DBCC FREEPROCCACHE";
            command.CommandText = stmt;
            await command.ExecuteNonQueryAsync();
            stmt = "DBCC FREESYSTEMCACHE ('SQL Plans');";
            command.CommandText = stmt;
            await command.ExecuteNonQueryAsync();
            for (int i = 0; i < request.TestsCount; i++)
            {
                stmt = @"SELECT Orders.Id as OrderId, Date, Status, Customers.Id as CustomerId, Customers.FirstName, Customers.LastName, Addresses.Country, Addresses.City, Addresses.State, Addresses.Street1, Addresses.Street2
        FROM Orders
        INNER JOIN Customers on Orders.CustomerId = Customers.Id
        INNER JOIN Addresses on Orders.AddressId = Addresses.Id
        Where Status = 'Awaiting Conformation'";
                command.CommandText = stmt;
                var stopWatch = new Stopwatch();
                List<object> orderIdsOb = new();
                stopWatch.Start();
                var reader = await command.ExecuteReaderAsync(cancellationToken);
                while (reader.Read())
                {
                    orderIdsOb.Add(reader["OrderId"]);
                }
                stopWatch.Stop();
                reader.Close();
                foreach (var item in orderIdsOb)
                {
                    var id = Guid.Parse(Convert.ToString(item)!);
                    stmt = @"SELECT ProductName, ProductPrice, Quantity, Percentage, Description
                         FROM Orders_Products
                         INNER JOIN Discounts on Discounts.Id = Orders_Products.ProductId
                         WHERE OrderId=@OrderId";
                    command.CommandText = stmt;
                    command.Parameters.AddWithValue("@OrderId", id);
                    var reader2 = await command.ExecuteReaderAsync(cancellationToken);
                    stopWatch.Start();
                    while (reader2.Read())
                    {
                    }
                    stopWatch.Stop();
                    reader2.Close();
                    command.Parameters.Clear();                    
                }
		result.Millis.Add((int)stopWatch.ElapsedMilliseconds); 
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