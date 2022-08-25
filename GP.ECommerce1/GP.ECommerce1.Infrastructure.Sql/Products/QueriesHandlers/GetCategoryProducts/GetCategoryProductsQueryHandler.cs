using System.Data.SqlClient;
using System.Diagnostics;
using GP.ECommerce1.Core.Application.Categories.Queries.GetCategories;
using GP.ECommerce1.Core.Application.Products.Queries.GetCategoryProducts;
using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Infrastructure.Sql.Products.QueriesHandlers.GetCategoryProducts;

public class
    GetCategoryProductsQueryHandler : IRequestHandler<GetCategoryProductsQuery,
        Result<GetCategoryProductsQueryResponse>>
{
    private readonly SqlConnection _connection;


    public GetCategoryProductsQueryHandler(SqlConnection connection)
    {
        _connection = connection;
    }

    public async Task<Result<GetCategoryProductsQueryResponse>> Handle(GetCategoryProductsQuery request,
        CancellationToken cancellationToken)
    {

        var result = new Result<GetCategoryProductsQueryResponse> {IsSuccess = true};
        string stmt = @"SELECT Products.Id, Price, Name, Products.MainImageUri, Discounts.Percentage as 'Discount'
        FROM Products
        INNER JOIN Discounts on Discounts.Id = Products.DiscountId
        Where CategoryId = @CategoryId";
        
        var command = new SqlCommand(stmt, _connection);
        command.Parameters.AddWithValue("@CategoryId", request.CategoryId);
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
                var entry = new GetCategoryProductsQueryResponseEntry
                {
                    Id = Guid.Parse(Convert.ToString(reader["Id"])),
                    Name = Convert.ToString(reader["Name"]),
                    Price = Convert.ToDouble(reader["Price"]),
                    MainImageUri = Convert.ToString(reader["MainImageUri"]),
                    Discount = Convert.ToDouble(reader["Discount"])
                };
                entries.Add(entry);
            }
            
            stopWatch.Stop();


            result.Value = new GetCategoryProductsQueryResponse {Products = entries};
            result.DatabaseActionSummary = stopWatch.ToDatabaseActionSummary(nameof(GetCategoriesQuery));
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