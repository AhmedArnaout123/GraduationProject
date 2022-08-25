using System.Data.SqlClient;
using GP.ECommerce1.Core.Application.Products.Queries.GetProducts;
using GP.ECommerce1.Core.Domain;
using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Infrastructure.Sql.Products.QueriesHandlers.GetProducts;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, Result<List<Product>>>
{
    private readonly SqlConnection _sqlConnection;

    public GetProductsQueryHandler(SqlConnection sqlConnection)
    {
        _sqlConnection = sqlConnection;
    }

    public async Task<Result<List<Product>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var result = new Result<List<Product>> {IsSuccess = true};
        List<Product> products = new();
        string stmt =
            @"SELECT Products.Id, Products.Name, Categories.Name as 'CategoryName', Discounts.Id, Discounts.Percentage as 'Discount', Description, MainImageUri, Price, CategoryId, DiscountId, RatesSum 
              FROM Products 
              INNER JOIN Categories on Categories.Id = Products.CategoryId
              Inner Join Discounts on Discounts.Id = Products.DiscountId";
        var command = new SqlCommand(stmt, _sqlConnection);
        try
        {
            _sqlConnection.Open();
            var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (reader.Read())
            {
                var product = new Product
                {
                    Id = Guid.Parse(Convert.ToString(reader["Id"])!),
                    Name = Convert.ToString(reader["Name"])!,
                    Description = Convert.ToString(reader["Description"])!,
                    CategoryId = Guid.Parse(Convert.ToString(reader["CategoryId"])!),
                    CategoryName = Convert.ToString(reader["CategoryName"])!,
                    DiscountId = Guid.TryParse(Convert.ToString(reader["DiscountId"]), out var r)
                        ? r
                        : null,
                    DiscountPercentage = double.TryParse(Convert.ToString(reader["Discount"]), out var x)
                        ? x
                        : null,
                    Price = Convert.ToDouble(reader["Price"]),
                    RatesSum = Convert.ToInt32(reader["RatesSum"]),
                    MainImageUri = Convert.ToString(reader["MainImageUri"])!
                };
                products.Add(product);
            }

            result.Value = products;
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