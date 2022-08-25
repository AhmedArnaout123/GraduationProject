using System.Data.SqlClient;
using GP.ECommerce1.Core.Application.Categories.Queries.GetCategories;
using GP.ECommerce1.Core.Domain;
using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Infrastructure.Sql.Categories.QueriesHandlers.GetCategories;

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, Result<List<Category>>>
{
    private readonly SqlConnection _sqlConnection;

    public GetCategoriesQueryHandler(SqlConnection sqlConnection)
    {
        _sqlConnection = sqlConnection;
    }

    public async Task<Result<List<Category>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var result = new Result<List<Category>> {IsSuccess = true};
        List<Category> categories = new();
        string stmt = "SELECT * FROM Categories";
        var command = new SqlCommand(stmt, _sqlConnection);
        try
        {
            _sqlConnection.Open();
            var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (reader.Read())
            {
                var category = new Category
                {
                    Id = Guid.Parse(Convert.ToString(reader["Id"])!),
                    Name = Convert.ToString(reader["Name"])!,
                    ParentId =  Guid.TryParse(Convert.ToString(reader["ParentId"]), out var r)
                        ? r
                        : null
                };
                categories.Add(category);
            }

            result.Value = categories;
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